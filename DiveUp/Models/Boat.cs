using System.ComponentModel.DataAnnotations;

namespace DiveUp.Models
{
    public class Boat
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string BoatName { get; set; } = string.Empty;

        public int? Capacity { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Active";

        [MaxLength(100)]
        public string? RecordBy { get; set; }

        public DateTime RecordTime { get; set; } = DateTime.Now;
    }
}
