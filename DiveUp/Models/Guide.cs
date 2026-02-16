using System.ComponentModel.DataAnnotations;
namespace DiveUp.Models
{
    public class Guide
    {
        public int Id { get; set; }
        [Required, MaxLength(200)] public string GuideName { get; set; } = string.Empty;
        [MaxLength(500)] public string? Address { get; set; }
        [MaxLength(50)]  public string? Phone { get; set; }
        public bool IsActive { get; set; } = true;
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
        public DateTime RecordTime { get; set; } = DateTime.UtcNow;
    }
}
