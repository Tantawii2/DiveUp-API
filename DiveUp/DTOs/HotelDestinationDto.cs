namespace DiveUp.DTOs
{
    public class HotelDestinationDto
    {
        public int Id { get; set; }
        public string DestinationName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string RecordBy { get; set; } = string.Empty;
        public DateTime RecordTime { get; set; }
    }
}
