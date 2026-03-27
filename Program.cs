using FINAL.Data;
using FINAL.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Invalidate all sessions when app restarts
builder.Services.AddDataProtection()
    .SetApplicationName(Guid.NewGuid().ToString());

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add ASP.NET Core Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/Login";
    options.Cookie.Name = "BloomsAndStitchAuth";
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// authentication before Authorization
app.UseAuthentication();
app.UseAuthorization();

// default route goes to Login first
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "user",
    pattern: "User/{action=Index}/{id?}",
    defaults: new { controller = "User" });

app.MapControllerRoute(
    name: "order",
    pattern: "Order/{action=Index}/{id?}",
    defaults: new { controller = "Order" });

app.MapControllerRoute(
    name: "dashboard",
    pattern: "Dashboard/{action=Index}/{id?}",
    defaults: new { controller = "Dashboard" });

// seed default Admin account
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

    // Seed Clark as Admin
    var adminEmail = "admin@gmail.com";
    var existing = await userManager.FindByEmailAsync(adminEmail);
    if (existing == null)
    {
        var admin = new AppUser
        {
            FullName = "Clark",
            UserName = "admin",
            Email = adminEmail,
            Role = "Admin",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(admin, "admin123");
    }
    else
    {
        existing.Role = "Admin";
        await userManager.UpdateAsync(existing);
    }

    // Seed Geraldine as Admin
    var geraldineEmail = "geraldine@gmail.com";
    var existingGeraldine = await userManager.FindByEmailAsync(geraldineEmail);
    if (existingGeraldine == null)
    {
        var geraldine = new AppUser
        {
            FullName = "Geraldine",
            UserName = "geraldine",
            Email = geraldineEmail,
            Role = "Admin",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(geraldine, "geraldine");
    }
    else
    {
        existingGeraldine.Role = "Admin";
        await userManager.UpdateAsync(existingGeraldine);
    }
}
app.Run();