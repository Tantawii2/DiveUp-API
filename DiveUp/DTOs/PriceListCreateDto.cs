using System.ComponentModel.DataAnnotations;
namespace DiveUp.DTOs
{
    public class PriceListCreateDto
    {
        [Required, MaxLength(200)] public string PriceListName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
    }
}
