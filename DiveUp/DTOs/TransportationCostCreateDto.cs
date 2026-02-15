using System.ComponentModel.DataAnnotations;

namespace DiveUp.DTOs
{
    public class TransportationCostCreateDto
    {
        public int? TypeId { get; set; }

        [Required(ErrorMessage = "Cost Value is required")]
        [Range(0, 999999)]
        public decimal CostValue { get; set; }

        [MaxLength(20)]
        public string Currency { get; set; } = "USD";

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        [MaxLength(100)]
        public string? RecordBy { get; set; }
    }
}
