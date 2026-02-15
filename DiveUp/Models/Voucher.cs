using System.ComponentModel.DataAnnotations;

namespace DiveUp.Models
{
    public class Voucher
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string VoucherFrom { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string VoucherTo { get; set; } = string.Empty;

        public int? VoucherCount { get; set; }

        public int? RepId { get; set; }
        public Rep? Rep { get; set; }

        [MaxLength(100)]
        public string? RecordBy { get; set; }

        public DateTime RecordTime { get; set; } = DateTime.Now;
    }
}
