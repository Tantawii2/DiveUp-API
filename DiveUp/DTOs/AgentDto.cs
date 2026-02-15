namespace DiveUp.DTOs
{
    public class AgentDto
    {
        public int Id { get; set; }
        public string AgentCode { get; set; } = string.Empty;
        public string AgentName { get; set; } = string.Empty;
        public string? Nationality { get; set; }
        public string? VatNo { get; set; }
        public string? FileNo { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? RecordBy { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
