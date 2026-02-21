using System.ComponentModel.DataAnnotations;
namespace DiveUp.DTOs.SystemOperation.Codes.Functions
{
    public class NationalityCreateDto
    {
        [Required, MaxLength(100)] public string NationalityName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
    }
}
