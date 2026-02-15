namespace DiveUp.DTOs
{
    public class TransportationCostDto
    {
        public int Id { get; set; }
        public int? TypeId { get; set; }
        public string? TypeName { get; set; }
        public decimal CostValue { get; set; }
        public string Currency { get; set; } = "USD";
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? RecordBy { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
