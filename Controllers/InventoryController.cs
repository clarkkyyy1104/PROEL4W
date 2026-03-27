using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FINAL.Data;
using FINAL.Models;
using FINAL.Controllers;

namespace FINAL.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public InventoryController(AppDbContext context, UserManager<AppUser> userManager)
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
            var items = await _context.InventoryItems.ToListAsync();
            return View(items);
        }

        // CREATE - Admin only
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventoryItem item)
        {
            if (await GetCurrentRole() != "Admin") return RedirectToAction("AccessDenied", "Account");
            if (ModelState.IsValid)
            {
                _context.InventoryItems.Add(item);
                await _context.SaveChangesAsync();
                if (item.IsLowStock) await AddNotification($"Low stock on {item.ItemName}! Only {item.Quantity} left.", "LowStock");
                TempData["Success"] = "Item added!";
            }
            return RedirectToAction(nameof(Index));
        }

        // EDIT - Admin only
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InventoryItem item)
        {
            if (await GetCurrentRole() != "Admin") return RedirectToAction("AccessDenied", "Account");
            if (id != item.Id) return NotFound();
            if (ModelState.IsValid)
            {
                var existing = await _context.InventoryItems.FindAsync(id);
                if (existing == null) return NotFound();
                bool wasOk = !existing.IsLowStock;
                existing.ItemName = item.ItemName; existing.Quantity = item.Quantity;
                existing.Category = item.Category; existing.Price = item.Price;
                existing.Description = item.Description;
                await _context.SaveChangesAsync();
                if (wasOk && existing.IsLowStock) await AddNotification($"Low stock on {existing.ItemName}! Only {existing.Quantity} left.", "LowStock");
                TempData["Success"] = "Item updated!";
            }
            return RedirectToAction(nameof(Index));
        }

        // DELETE - Admin only
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await GetCurrentRole() != "Admin") return RedirectToAction("AccessDenied", "Account");
            var item = await _context.InventoryItems.FindAsync(id);
            if (item != null) { _context.InventoryItems.Remove(item); await _context.SaveChangesAsync(); TempData["Success"] = "Item deleted!"; }
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> GetCurrentRole()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Role ?? "Staff";
        }

        private async Task AddNotification(string message, string type)
        {
            _context.Notifications.Add(new Notification { Message = message, Type = type, CreatedAt = DateTime.Now, IsRead = false });
            await _context.SaveChangesAsync();
        }
    }
}
