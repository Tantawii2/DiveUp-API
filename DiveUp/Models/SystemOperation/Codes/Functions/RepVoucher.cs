using System.ComponentModel.DataAnnotations;
namespace DiveUp.Models.SystemOperation.Codes.Functions
{
    public class RepVoucher
    {
        public int Id { get; set; }
        public int? RepId { get; set; }
        public Rep? Rep { get; set; }
        [Required] public int FromNumber { get; set; }
        [Required] public int ToNumber { get; set; }
        public int CountVouchers { get; set; }
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
        public DateTime RecordTime { get; set; } = DateTime.UtcNow;
    }
}
