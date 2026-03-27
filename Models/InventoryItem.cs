using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FINAL.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public int LowStockThreshold { get; set; } = 10;

        [Required(ErrorMessage = "Item name is required.")]
        [StringLength(200)]
        public string ItemName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be 0 or more.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be 0 or more.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsLowStock { get; internal set; }
    }
}
