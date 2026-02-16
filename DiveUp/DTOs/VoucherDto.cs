namespace DiveUp.DTOs
{
    public class VoucherDto
    {
        public int Id { get; set; }
        public int? RepId { get; set; }
        public string? RepName { get; set; }
        public int FromNumber { get; set; }
        public int ToNumber { get; set; }
        public int CountVouchers { get; set; }
        public string RecordBy { get; set; } = string.Empty;
        public DateTime RecordTime { get; set; }
    }
}
