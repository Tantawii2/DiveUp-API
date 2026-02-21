using System.ComponentModel.DataAnnotations;
namespace DiveUp.DTOs.SystemOperation.Codes.Functions
{
    public class RateCreateDto
    {
        [Required] public DateTime FromDate { get; set; }
        [Required] public DateTime ToDate { get; set; }
        [Required, MaxLength(10)] public string Currency { get; set; } = "EGP";
        [Required, Range(0, double.MaxValue)] public decimal RateValue { get; set; }
    }
}
