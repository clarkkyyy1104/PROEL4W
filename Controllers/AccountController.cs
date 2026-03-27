using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FINAL.Data;
using FINAL.Models;
using Microsoft.AspNetCore.Authorization;

namespace FINAL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard");
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Support login by email OR username
            var user = await _userManager.FindByEmailAsync(model.EmailOrUsername)
                       ?? await _userManager.FindByNameAsync(model.EmailOrUsername);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email/username or password.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Dashboard");

            ModelState.AddModelError("", "Invalid email/username or password.");
            return View(model);
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            if (User.Identity!.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard");
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var existingEmail = await _userManager.FindByEmailAsync(model.Email);
            if (existingEmail != null)
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                return View(model);
            }

            var existingUsername = await _userManager.FindByNameAsync(model.Username);
            if (existingUsername != null)
            {
                ModelState.AddModelError("Username", "This username is already taken.");
                return View(model);
            }

            var user = new AppUser
            {
                FullName = model.FullName,
                UserName = model.Username,
                Email = model.Email,
                Role = "Staff"
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Add to User Management table
                _context.Users.Add(new User
                {
                    Name = model.FullName,
                    Role = "Staff",
                    Status = "Active"
                });

                // Add notification
                _context.Notifications.Add(new Notification
                {
                    Message = $"A new user '{model.FullName}' (@{model.Username}) has registered.",
                    Type = "NewUser",
                    CreatedAt = DateTime.Now,
                    IsRead = false
                });

                await _context.SaveChangesAsync();

                TempData["Success"] = "Account created successfully! Please sign in.";
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
    
}
