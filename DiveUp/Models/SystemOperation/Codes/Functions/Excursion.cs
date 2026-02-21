using System.ComponentModel.DataAnnotations;
namespace DiveUp.Models.SystemOperation.Codes.Functions
{
    public class Excursion
    {
        public int Id { get; set; }
        [Required, MaxLength(200)] public string ExcursionName { get; set; } = string.Empty;
        public int? SupplierId { get; set; }
        public ExcursionSupplier? Supplier { get; set; }
        public bool IsActive { get; set; } = true;
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
        public DateTime RecordTime { get; set; } = DateTime.UtcNow;
    }
}
