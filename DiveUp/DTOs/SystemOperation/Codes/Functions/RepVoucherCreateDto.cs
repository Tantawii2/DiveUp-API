using System.ComponentModel.DataAnnotations;
namespace DiveUp.DTOs.SystemOperation.Codes.Functions
{
    public class RepVoucherCreateDto
    {
        public int? RepId { get; set; }
        [Required, Range(1, int.MaxValue)] public int FromNumber { get; set; }
        [Required, Range(1, int.MaxValue)] public int ToNumber { get; set; }
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
    }
}
