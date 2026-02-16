using System.ComponentModel.DataAnnotations;
namespace DiveUp.DTOs
{
    public class TransportationSupplierCreateDto
    {
        [Required, MaxLength(200)] public string SupplierName { get; set; } = string.Empty;
        [MaxLength(50)]  public string? VatNo { get; set; }
        [MaxLength(50)]  public string? FileNo { get; set; }
        [EmailAddress, MaxLength(200)] public string? Email { get; set; }
        [MaxLength(500)] public string? Address { get; set; }
        [MaxLength(50)]  public string? Phone { get; set; }
        public bool IsActive { get; set; } = true;
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
    }
}
