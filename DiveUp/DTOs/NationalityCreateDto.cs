using System.ComponentModel.DataAnnotations;

namespace DiveUp.DTOs
{
    public class NationalityCreateDto
    {
        [Required(ErrorMessage = "Nationality Name is required")]
        [MaxLength(100)]
        public string NationalityName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? RecordBy { get; set; }
    }
}
