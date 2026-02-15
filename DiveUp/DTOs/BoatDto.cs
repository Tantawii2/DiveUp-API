namespace DiveUp.DTOs
{
    public class BoatDto
    {
        public int Id { get; set; }
        public string BoatName { get; set; } = string.Empty;
        public int? Capacity { get; set; }
        public string Status { get; set; } = "Active";
        public string? RecordBy { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
