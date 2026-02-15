using System.ComponentModel.DataAnnotations;

namespace DiveUp.DTOs
{
    public class HotelCreateDto
    {
        [Required(ErrorMessage = "Hotel Name is required")]
        [MaxLength(200)]
        public string HotelName { get; set; } = string.Empty;

        public int? DestinationId { get; set; }

        [MaxLength(100)]
        public string? RecordBy { get; set; }
    }
}
