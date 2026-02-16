using System.ComponentModel.DataAnnotations;
namespace DiveUp.Models
{
    public class Nationality
    {
        public int Id { get; set; }
        [Required, MaxLength(100)] public string NationalityName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
        public DateTime RecordTime { get; set; } = DateTime.UtcNow;
    }
}
