using System.ComponentModel.DataAnnotations;
namespace DiveUp.Models
{
    public class TransportationType
    {
        public int Id { get; set; }
        [Required, MaxLength(200)] public string TypeName { get; set; } = string.Empty;
        public int? SupplierId { get; set; }
        public TransportationSupplier? Supplier { get; set; }
        public bool IsActive { get; set; } = true;
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
        public DateTime RecordTime { get; set; } = DateTime.UtcNow;
    }
}
