using System.ComponentModel.DataAnnotations;

namespace FINAL.Models
{
    public class Notification
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // "auto" or "manual"
        public string Type { get; set; } = "manual";
    }
}
