using System.ComponentModel.DataAnnotations;

namespace FINAL.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product/Item is required.")]
        [StringLength(200)]
        public string Product { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Pending";

        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
