namespace DiveUp.DTOs.SystemOperation.Codes.Functions
{
    public class RepVoucherDto
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
