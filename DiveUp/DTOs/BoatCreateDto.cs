using System.ComponentModel.DataAnnotations;

namespace DiveUp.DTOs
{
    public class BoatCreateDto
    {
        [Required(ErrorMessage = "Boat Name is required")]
        [MaxLength(200)]
        public string BoatName { get; set; } = string.Empty;

        [Range(1, 1000)]
        public int? Capacity { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Active";

        [MaxLength(100)]
        public string? RecordBy { get; set; }
    }
}
