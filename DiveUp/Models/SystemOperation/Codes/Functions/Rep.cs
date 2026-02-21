using System.ComponentModel.DataAnnotations;
namespace DiveUp.Models.SystemOperation.Codes.Functions
{
    public class Rep
    {
        public int Id { get; set; }
        [Required, MaxLength(200)] public string RepName { get; set; } = string.Empty;
        public int? AgentId { get; set; }
        public Agent? Agent { get; set; }
        [MaxLength(500)] public string? Address { get; set; }
        [MaxLength(50)]  public string? Phone { get; set; }
        public bool IsActive { get; set; } = true;
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
        public DateTime RecordTime { get; set; } = DateTime.UtcNow;
    }
}
