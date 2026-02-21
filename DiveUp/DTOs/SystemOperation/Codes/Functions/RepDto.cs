namespace DiveUp.DTOs.SystemOperation.Codes.Functions
{
    public class RepDto
    {
        public int Id { get; set; }
        public string RepName { get; set; } = string.Empty;
        public int? AgentId { get; set; }
        public string? AgentName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public string RecordBy { get; set; } = string.Empty;
        public DateTime RecordTime { get; set; }
    }
}
