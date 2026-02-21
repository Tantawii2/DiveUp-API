using System.ComponentModel.DataAnnotations;
namespace DiveUp.Models.SystemOperation.Codes.Functions
{
    public class TransportationCost
    {
        public int Id { get; set; }
        public int? SupplierId { get; set; }
        public TransportationSupplier? Supplier { get; set; }
        public int? CarTypeId { get; set; }
        public TransportationType? CarType { get; set; }
        public int? DestinationId { get; set; }
        public HotelDestination? Destination { get; set; }
        // "One Way" or "Two Way"
        [Required, MaxLength(20)] public string RoundType { get; set; } = "One Way";
        [Required] public decimal CostEGP { get; set; }
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
        public DateTime RecordTime { get; set; } = DateTime.UtcNow;
    }
}
