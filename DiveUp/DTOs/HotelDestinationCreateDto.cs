using System.ComponentModel.DataAnnotations;

namespace DiveUp.DTOs
{
    public class HotelDestinationCreateDto
    {
        [Required(ErrorMessage = "Destination Name is required")]
        [MaxLength(200)]
        public string DestinationName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? RecordBy { get; set; }
    }
}
