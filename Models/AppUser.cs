using Microsoft.AspNetCore.Identity;

namespace FINAL.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = "Staff";
    }
}