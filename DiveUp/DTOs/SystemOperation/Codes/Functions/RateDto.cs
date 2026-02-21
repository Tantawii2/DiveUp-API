namespace DiveUp.DTOs.SystemOperation.Codes.Functions
{
    public class RateDto
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Currency { get; set; } = string.Empty;
        public decimal RateValue { get; set; }
    }
}
