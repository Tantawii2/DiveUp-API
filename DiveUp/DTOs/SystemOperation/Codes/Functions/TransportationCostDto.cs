namespace DiveUp.DTOs.SystemOperation.Codes.Functions
{
    public class TransportationCostDto
    {
        public int Id { get; set; }
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public int? CarTypeId { get; set; }
        public string? CarTypeName { get; set; }
        public int? DestinationId { get; set; }
        public string? DestinationName { get; set; }
        public string RoundType { get; set; } = string.Empty;
        public decimal CostEGP { get; set; }
        public string RecordBy { get; set; } = string.Empty;
        public DateTime RecordTime { get; set; }
    }
}
