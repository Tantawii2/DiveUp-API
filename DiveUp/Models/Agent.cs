using System.ComponentModel.DataAnnotations;

namespace DiveUp.Models
{
    public class Agent
    {
        public int Id { get; set; }

        [Required, MaxLength(20)]
        public string AgentCode { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string AgentName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Nationality { get; set; }

        [MaxLength(50)]
        public string? VatNo { get; set; }

        [MaxLength(50)]
        public string? FileNo { get; set; }

        [MaxLength(200)]
        public string? Email { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? RecordBy { get; set; }

        public DateTime RecordTime { get; set; } = DateTime.Now;
    }
}
