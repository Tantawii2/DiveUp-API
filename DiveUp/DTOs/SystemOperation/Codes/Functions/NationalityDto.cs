namespace DiveUp.DTOs.SystemOperation.Codes.Functions
{
    public class NationalityDto
    {
        public int Id { get; set; }
        public string NationalityName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string RecordBy { get; set; } = string.Empty;
        public DateTime RecordTime { get; set; }
    }
}
