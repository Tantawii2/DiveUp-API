namespace DiveUp.DTOs
{
    public class NationalityDto
    {
        public int Id { get; set; }
        public string NationalityName { get; set; } = string.Empty;
        public string? RecordBy { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
