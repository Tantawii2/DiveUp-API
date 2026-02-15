using System.ComponentModel.DataAnnotations;

namespace DiveUp.Models
{
    public class PriceList
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string PriceListName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? RecordBy { get; set; }

        public DateTime RecordTime { get; set; } = DateTime.Now;
    }
}
