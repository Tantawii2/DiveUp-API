namespace DiveUp.DTOs
{
    public class RateDto
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Currency { get; set; } = "USD";
        public decimal RateValue { get; set; }
        public string? RecordBy { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
