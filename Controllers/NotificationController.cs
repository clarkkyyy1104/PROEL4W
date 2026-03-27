using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FINAL.Data;
using FINAL.Models;

namespace FINAL.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public NotificationController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // READ - All roles except Farmer
        public async Task<IActionResult> Index()
        {
            var role = await GetCurrentRole();
            if (role == "Farmer") return RedirectToAction("AccessDenied", "Account");
            ViewBag.IsAdmin = role == "Admin";
            var notifications = await _context.Notifications.OrderByDescending(n => n.CreatedAt).ToListAsync();
            return View(notifications);
        }

        // CREATE - Admin only
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string message)
        {
            if (await GetCurrentRole() != "Admin") return RedirectToAction("AccessDenied", "Account");
            if (!string.IsNullOrWhiteSpace(message))
            {
                _context.Notifications.Add(new Notification { Message = message, Type = "Manual", CreatedAt = DateTime.Now, IsRead = false });
                await _context.SaveChangesAsync();
                TempData["Success"] = "Notification added!";
            }
            return RedirectToAction(nameof(Index));
        }

        // TOGGLE READ - All roles except Farmer
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleRead(int id)
        {
            if (await GetCurrentRole() == "Farmer") return RedirectToAction("AccessDenied", "Account");
            var notif = await _context.Notifications.FindAsync(id);
            if (notif != null) { notif.IsRead = !notif.IsRead; await _context.SaveChangesAsync(); }
            return RedirectToAction(nameof(Index));
        }

        // DELETE - Admin only
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await GetCurrentRole() != "Admin") return RedirectToAction("AccessDenied", "Account");
            var notif = await _context.Notifications.FindAsync(id);
            if (notif != null) { _context.Notifications.Remove(notif); await _context.SaveChangesAsync(); TempData["Success"] = "Notification deleted!"; }
            return RedirectToAction(nameof(Index));
        }

        // MARK ALL READ - All roles except Farmer
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAllRead()
        {
            if (await GetCurrentRole() == "Farmer") return RedirectToAction("AccessDenied", "Account");
            var unread = await _context.Notifications.Where(n => !n.IsRead).ToListAsync();
            foreach (var n in unread) n.IsRead = true;
            await _context.SaveChangesAsync();
            TempData["Success"] = "All marked as read!";
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> GetCurrentRole()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Role ?? "Staff";
        }
    }
}
