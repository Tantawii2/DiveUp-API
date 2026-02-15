using System.ComponentModel.DataAnnotations;

namespace DiveUp.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string HotelName { get; set; } = string.Empty;

        public int? DestinationId { get; set; }
        public HotelDestination? Destination { get; set; }

        [MaxLength(100)]
        public string? RecordBy { get; set; }

        public DateTime RecordTime { get; set; } = DateTime.Now;
    }
}
