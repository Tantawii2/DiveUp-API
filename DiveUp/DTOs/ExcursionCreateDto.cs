using System.ComponentModel.DataAnnotations;

namespace DiveUp.DTOs
{
    public class ExcursionCreateDto
    {
        [Required(ErrorMessage = "Excursion Name is required")]
        [MaxLength(200)]
        public string ExcursionName { get; set; } = string.Empty;

        public int? SupplierId { get; set; }

        [MaxLength(100)]
        public string? RecordBy { get; set; }
    }
}
