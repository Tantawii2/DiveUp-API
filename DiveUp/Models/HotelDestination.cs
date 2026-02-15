using System.ComponentModel.DataAnnotations;

namespace DiveUp.Models
{
    public class HotelDestination
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string DestinationName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? RecordBy { get; set; }

        public DateTime RecordTime { get; set; } = DateTime.Now;
    }
}
