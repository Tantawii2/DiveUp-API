using System.ComponentModel.DataAnnotations;

namespace DiveUp.Models
{
    public class TransportationSupplier
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string SupplierName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? RecordBy { get; set; }

        public DateTime RecordTime { get; set; } = DateTime.Now;
    }
}
