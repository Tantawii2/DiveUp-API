namespace DiveUp.DTOs
{
    public class BoatDto
    {
        public int Id { get; set; }
        public string BoatName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string RecordBy { get; set; } = string.Empty;
        public DateTime RecordTime { get; set; }
    }
}
