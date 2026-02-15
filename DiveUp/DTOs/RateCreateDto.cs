using System.ComponentModel.DataAnnotations;

namespace DiveUp.DTOs
{
    public class RateCreateDto
    {
        [Required(ErrorMessage = "From Date is required")]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "To Date is required")]
        public DateTime ToDate { get; set; }

        [MaxLength(20)]
        public string Currency { get; set; } = "USD";

        [Required(ErrorMessage = "Rate Value is required")]
        [Range(0.0001, 999999)]
        public decimal RateValue { get; set; }

        [MaxLength(100)]
        public string? RecordBy { get; set; }
    }
}
