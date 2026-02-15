using System.ComponentModel.DataAnnotations;

namespace DiveUp.Models
{
    public class Rate
    {
        public int Id { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [MaxLength(20)]
        public string Currency { get; set; } = "USD";

        [Required]
        public decimal RateValue { get; set; }

        [MaxLength(100)]
        public string? RecordBy { get; set; }

        public DateTime RecordTime { get; set; } = DateTime.Now;
    }
}
