using System.ComponentModel.DataAnnotations;
namespace DiveUp.DTOs.SystemOperation.Codes.Functions
{
    public class ExcursionCreateDto
    {
        [Required, MaxLength(200)] public string ExcursionName { get; set; } = string.Empty;
        public int? SupplierId { get; set; }
        public bool IsActive { get; set; } = true;
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
    }
}
