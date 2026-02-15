namespace DiveUp.DTOs
{
    public class VoucherDto
    {
        public int Id { get; set; }
        public string VoucherFrom { get; set; } = string.Empty;
        public string VoucherTo { get; set; } = string.Empty;
        public int? VoucherCount { get; set; }
        public int? RepId { get; set; }
        public string? RepName { get; set; }
        public string? RecordBy { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
