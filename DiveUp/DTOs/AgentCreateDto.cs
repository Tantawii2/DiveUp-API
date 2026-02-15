using System.ComponentModel.DataAnnotations;

namespace DiveUp.DTOs
{
    public class AgentCreateDto
    {
        [Required(ErrorMessage = "Agent Code is required")]
        [MaxLength(20)]
        public string AgentCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Agent Name is required")]
        [MaxLength(200)]
        public string AgentName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Nationality { get; set; }

        [MaxLength(50)]
        public string? VatNo { get; set; }

        [MaxLength(50)]
        public string? FileNo { get; set; }

        [EmailAddress]
        [MaxLength(200)]
        public string? Email { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        [MaxLength(50)]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? RecordBy { get; set; }
    }
}
