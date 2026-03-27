using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FINAL.Data;
using FINAL.Models;

namespace FINAL.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var role = currentUser?.Role ?? "Staff";

            ViewBag.CurrentUserName = currentUser?.FullName ?? currentUser?.UserName ?? "User";
            ViewBag.CurrentUserRole = role;

            // Admin gets full stats
            if (role == "Admin")
            {
                ViewBag.TotalUsers = await _context.Users.CountAsync();
                ViewBag.TotalOrders = await _context.Orders.CountAsync();
                ViewBag.PendingOrders = await _context.Orders.CountAsync(o => o.Status == "Pending");
                ViewBag.CompletedOrders = await _context.Orders.CountAsync(o => o.Status == "Completed");
                ViewBag.TotalInventory = await _context.InventoryItems.CountAsync();
                ViewBag.LowStockCount = await _context.InventoryItems.CountAsync(i => i.Quantity <= i.LowStockThreshold);
                ViewBag.UnreadNotifications = await _context.Notifications.CountAsync(n => !n.IsRead);
                return View("AdminDashboard");
            }

            // Farmer gets minimal dashboard
            if (role == "Farmer")
            {
                return View("FarmerDashboard");
            }

            // Staff, Florist, Sales Associate get view-only dashboard
            ViewBag.TotalOrders = await _context.Orders.CountAsync();
            ViewBag.PendingOrders = await _context.Orders.CountAsync(o => o.Status == "Pending");
            ViewBag.TotalInventory = await _context.InventoryItems.CountAsync();
            ViewBag.LowStockCount = await _context.InventoryItems.CountAsync(i => i.Quantity <= i.LowStockThreshold);
            return View("StaffDashboard");
        }
    }
}
