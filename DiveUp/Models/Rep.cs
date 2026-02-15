using System.ComponentModel.DataAnnotations;

namespace DiveUp.Models
{
    public class Rep
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string RepName { get; set; } = string.Empty;

        public int? AgentId { get; set; }
        public Agent? Agent { get; set; }

        [MaxLength(100)]
        public string? RecordBy { get; set; }

        public DateTime RecordTime { get; set; } = DateTime.Now;
    }
}
