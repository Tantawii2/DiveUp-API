using System.ComponentModel.DataAnnotations;
namespace DiveUp.Models.SystemOperation.Codes.Functions
{
    public class Boat
    {
        public int Id { get; set; }
        [Required, MaxLength(200)] public string BoatName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
        public DateTime RecordTime { get; set; } = DateTime.UtcNow;
    }
}
