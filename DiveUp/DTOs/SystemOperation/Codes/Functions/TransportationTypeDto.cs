namespace DiveUp.DTOs.SystemOperation.Codes.Functions
{
    public class TransportationTypeDto
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public bool IsActive { get; set; }
        public string RecordBy { get; set; } = string.Empty;
        public DateTime RecordTime { get; set; }
    }
}
