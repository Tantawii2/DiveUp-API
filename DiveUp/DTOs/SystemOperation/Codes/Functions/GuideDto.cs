namespace DiveUp.DTOs.SystemOperation.Codes.Functions
{
    public class GuideDto
    {
        public int Id { get; set; }
        public string GuideName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public string RecordBy { get; set; } = string.Empty;
        public DateTime RecordTime { get; set; }
    }
}
