namespace DiveUp.DTOs
{
    public class ExcursionSupplierDto
    {
        public int Id { get; set; }
        public string SupplierName { get; set; } = string.Empty;
        public string? RecordBy { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
