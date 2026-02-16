namespace DiveUp.DTOs
{
    public class PriceListDto
    {
        public int Id { get; set; }
        public string PriceListName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string RecordBy { get; set; } = string.Empty;
        public DateTime RecordTime { get; set; }
    }
}
