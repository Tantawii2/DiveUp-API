using System.ComponentModel.DataAnnotations;

namespace DiveUp.DTOs
{
    public class PriceListCreateDto
    {
        [Required(ErrorMessage = "Price List Name is required")]
        [MaxLength(200)]
        public string PriceListName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? RecordBy { get; set; }
    }
}
