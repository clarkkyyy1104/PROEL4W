using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FINAL.Data;
using FINAL.Models;

namespace FINAL.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // READ - Admin only
        public async Task<IActionResult> Index()
        {
            var role = await GetCurrentRole();
            if (role != "Admin") return RedirectToAction("AccessDenied", "Account");
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (await GetCurrentRole() != "Admin") return RedirectToAction("AccessDenied", "Account");
            if (ModelState.IsValid) { _context.Users.Add(user); await _context.SaveChangesAsync(); TempData["Success"] = "User added!"; }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (await GetCurrentRole() != "Admin") return RedirectToAction("AccessDenied", "Account");
            if (id != user.Id) return NotFound();
            if (ModelState.IsValid) { _context.Users.Update(user); await _context.SaveChangesAsync(); TempData["Success"] = "User updated!"; }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await GetCurrentRole() != "Admin") return RedirectToAction("AccessDenied", "Account");
            var user = await _context.Users.FindAsync(id);
            if (user != null) { _context.Users.Remove(user); await _context.SaveChangesAsync(); TempData["Success"] = "User deleted!"; }
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> GetCurrentRole()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Role ?? "Staff";
        }
    }
}
