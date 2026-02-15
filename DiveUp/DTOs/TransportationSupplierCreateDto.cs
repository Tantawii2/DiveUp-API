using System.ComponentModel.DataAnnotations;

namespace DiveUp.DTOs
{
    public class TransportationSupplierCreateDto
    {
        [Required(ErrorMessage = "Supplier Name is required")]
        [MaxLength(200)]
        public string SupplierName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? RecordBy { get; set; }
    }
}
