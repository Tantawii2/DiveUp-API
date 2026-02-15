using System.ComponentModel.DataAnnotations;

namespace DiveUp.DTOs
{
    public class RepCreateDto
    {
        [Required(ErrorMessage = "Rep Name is required")]
        [MaxLength(200)]
        public string RepName { get; set; } = string.Empty;

        public int? AgentId { get; set; }

        [MaxLength(100)]
        public string? RecordBy { get; set; }
    }
}
