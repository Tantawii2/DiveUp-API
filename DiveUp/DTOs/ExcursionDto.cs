namespace DiveUp.DTOs
{
    public class ExcursionDto
    {
        public int Id { get; set; }
        public string ExcursionName { get; set; } = string.Empty;
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public bool IsActive { get; set; }
        public string RecordBy { get; set; } = string.Empty;
        public DateTime RecordTime { get; set; }
    }
}
