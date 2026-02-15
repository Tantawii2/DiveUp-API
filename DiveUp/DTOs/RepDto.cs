namespace DiveUp.DTOs
{
    public class RepDto
    {
        public int Id { get; set; }
        public string RepName { get; set; } = string.Empty;
        public int? AgentId { get; set; }
        public string? AgentName { get; set; }
        public string? RecordBy { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
