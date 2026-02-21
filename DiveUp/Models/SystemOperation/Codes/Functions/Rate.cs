using System.ComponentModel.DataAnnotations;
namespace DiveUp.Models.SystemOperation.Codes.Functions
{
    public class Rate
    {
        public int Id { get; set; }
        [Required] public DateTime FromDate { get; set; }
        [Required] public DateTime ToDate { get; set; }
        [Required, MaxLength(10)] public string Currency { get; set; } = "EGP";
        [Required] public decimal RateValue { get; set; }
    }
}
