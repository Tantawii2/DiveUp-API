using System.ComponentModel.DataAnnotations;
namespace DiveUp.DTOs
{
    public class HotelDestinationCreateDto
    {
        [Required, MaxLength(200)] public string DestinationName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
    }
}
