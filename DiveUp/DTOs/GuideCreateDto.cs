using System.ComponentModel.DataAnnotations;

namespace DiveUp.DTOs
{
    public class GuideCreateDto
    {
        [Required(ErrorMessage = "Guide Name is required")]
        [MaxLength(200)]
        public string GuideName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? RecordBy { get; set; }
    }
}
