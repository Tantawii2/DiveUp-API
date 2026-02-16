using System.ComponentModel.DataAnnotations;
namespace DiveUp.DTOs
{
    public class TransportationCostCreateDto
    {
        public int? SupplierId { get; set; }
        public int? CarTypeId { get; set; }
        public int? DestinationId { get; set; }
        [Required, RegularExpression("One Way|Two Way", ErrorMessage = "RoundType must be 'One Way' or 'Two Way'")]
        public string RoundType { get; set; } = "One Way";
        [Required, Range(0, double.MaxValue)] public decimal CostEGP { get; set; }
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
    }
}
