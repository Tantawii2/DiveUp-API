using System.ComponentModel.DataAnnotations;

namespace DiveUp.Models
{
    public class Excursion
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string ExcursionName { get; set; } = string.Empty;

        public int? SupplierId { get; set; }
        public ExcursionSupplier? Supplier { get; set; }

        [MaxLength(100)]
        public string? RecordBy { get; set; }

        public DateTime RecordTime { get; set; } = DateTime.Now;
    }
}
