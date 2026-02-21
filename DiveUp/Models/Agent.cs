using System.ComponentModel.DataAnnotations;
namespace DiveUp.Models
{
    public class Agent
    {
        public int Id { get; set; }
        [Required, MaxLength(20)]  public string AgentCode { get; set; } = string.Empty;
        [Required, MaxLength(200)] public string AgentName { get; set; } = string.Empty;
        public int? NationalityId { get; set; }
        public Nationality? Nationality { get; set; }
        [MaxLength(50)]  public string? VatNo { get; set; }
        [MaxLength(50)]  public string? FileNo { get; set; }
        [MaxLength(200)] public string? Email { get; set; }
        [MaxLength(500)] public string? Address { get; set; }
        [MaxLength(50)]  public string? Phone { get; set; }
        public bool IsActive { get; set; } = true;
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
        public DateTime RecordTime { get; set; } = DateTime.UtcNow;
    }
}
