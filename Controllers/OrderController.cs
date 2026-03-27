using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FINAL.Data;
using FINAL.Models;

namespace FINAL.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(AppDbContext context, UserManager<AppUser> userManager)
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
            var orders = await _context.Orders.ToListAsync();
            return View(orders);
        }

        // CREATE - Admin only
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Order order)
        {
            if (await GetCurrentRole() != "Admin") return RedirectToAction("AccessDenied", "Account");
            if (ModelState.IsValid) { order.OrderDate = DateTime.Now; _context.Orders.Add(order); await _context.SaveChangesAsync(); TempData["Success"] = "Order added!"; }
            return RedirectToAction(nameof(Index));
        }

        // EDIT - Admin only
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (await GetCurrentRole() != "Admin") return RedirectToAction("AccessDenied", "Account");
            if (id != order.Id) return NotFound();
            if (ModelState.IsValid) { _context.Orders.Update(order); await _context.SaveChangesAsync(); TempData["Success"] = "Order updated!"; }
            return RedirectToAction(nameof(Index));
        }

        // DELETE - Admin only
        [HttpPost][ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (await GetCurrentRole() != "Admin") return RedirectToAction("AccessDenied", "Account");
            var order = await _context.Orders.FindAsync(id);
            if (order != null) { _context.Orders.Remove(order); await _context.SaveChangesAsync(); TempData["Success"] = "Order deleted!"; }
            return RedirectToAction(nameof(Index));
        }

        private async Task<string> GetCurrentRole()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Role ?? "Staff";
        }
    }
}
