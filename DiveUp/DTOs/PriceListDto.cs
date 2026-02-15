namespace DiveUp.DTOs
{
    public class PriceListDto
    {
        public int Id { get; set; }
        public string PriceListName { get; set; } = string.Empty;
        public string? RecordBy { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
