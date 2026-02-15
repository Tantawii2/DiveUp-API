namespace DiveUp.DTOs
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public int? DestinationId { get; set; }
        public string? DestinationName { get; set; }
        public string? RecordBy { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
