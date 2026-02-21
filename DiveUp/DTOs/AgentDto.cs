namespace DiveUp.DTOs
{
    public class AgentDto
    {
        public int Id { get; set; }
        public string AgentCode { get; set; } = string.Empty;
        public string AgentName { get; set; } = string.Empty;
        public int? NationalityId { get; set; }
        public string? NationalityName { get; set; }
        public string? VatNo { get; set; }
        public string? FileNo { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public string RecordBy { get; set; } = string.Empty;
        public DateTime RecordTime { get; set; }
    }
}
