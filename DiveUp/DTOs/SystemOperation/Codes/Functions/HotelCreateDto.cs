using System.ComponentModel.DataAnnotations;
namespace DiveUp.DTOs.SystemOperation.Codes.Functions
{
    public class HotelCreateDto
    {
        [Required, MaxLength(200)] public string HotelName { get; set; } = string.Empty;
        public int? DestinationId { get; set; }
        public bool IsActive { get; set; } = true;
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
    }
}
