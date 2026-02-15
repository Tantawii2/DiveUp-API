using System.ComponentModel.DataAnnotations;

namespace DiveUp.Models
{
    public class TransportationCost
    {
        public int Id { get; set; }

        public int? TypeId { get; set; }
        public TransportationType? Type { get; set; }

        [Required]
        public decimal CostValue { get; set; }

        [MaxLength(20)]
        public string Currency { get; set; } = "USD";

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        [MaxLength(100)]
        public string? RecordBy { get; set; }

        public DateTime RecordTime { get; set; } = DateTime.Now;
    }
}
