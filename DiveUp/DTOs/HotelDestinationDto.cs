namespace DiveUp.DTOs
{
    public class HotelDestinationDto
    {
        public int Id { get; set; }
        public string DestinationName { get; set; } = string.Empty;
        public string? RecordBy { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
