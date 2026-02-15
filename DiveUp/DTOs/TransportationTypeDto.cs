namespace DiveUp.DTOs
{
    public class TransportationTypeDto
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? RecordBy { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
