using System.ComponentModel.DataAnnotations;

namespace DiveUp.Models
{
    public class Nationality
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string NationalityName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? RecordBy { get; set; }

        public DateTime RecordTime { get; set; } = DateTime.Now;
    }
}
