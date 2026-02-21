using System.ComponentModel.DataAnnotations;
namespace DiveUp.DTOs
{
    public class GuideCreateDto
    {
        [Required, MaxLength(200)] public string GuideName { get; set; } = string.Empty;
        [MaxLength(500)] public string? Address { get; set; }
        [MaxLength(50)]  public string? Phone { get; set; }
        public bool IsActive { get; set; } = true;
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
    }
}
