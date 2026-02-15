using System.ComponentModel.DataAnnotations;

namespace DiveUp.DTOs
{
    public class TransportationTypeCreateDto
    {
        [Required(ErrorMessage = "Type Name is required")]
        [MaxLength(200)]
        public string TypeName { get; set; } = string.Empty;

        public int? SupplierId { get; set; }

        [MaxLength(100)]
        public string? RecordBy { get; set; }
    }
}
