namespace DiveUp.DTOs
{
    public class HotelDto
    {
        public int Id { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public int? DestinationId { get; set; }
        public string? DestinationName { get; set; }
        public bool IsActive { get; set; }
        public string RecordBy { get; set; } = string.Empty;
        public DateTime RecordTime { get; set; }
    }
}
