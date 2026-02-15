using System.ComponentModel.DataAnnotations;

namespace DiveUp.DTOs
{
    public class VoucherCreateDto
    {
        [Required(ErrorMessage = "From is required")]
        [MaxLength(50)]
        public string VoucherFrom { get; set; } = string.Empty;

        [Required(ErrorMessage = "To is required")]
        [MaxLength(50)]
        public string VoucherTo { get; set; } = string.Empty;

        public int? VoucherCount { get; set; }

        public int? RepId { get; set; }

        [MaxLength(100)]
        public string? RecordBy { get; set; }
    }
}
