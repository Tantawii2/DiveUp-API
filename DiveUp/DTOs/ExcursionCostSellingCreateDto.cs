using System.ComponentModel.DataAnnotations;
namespace DiveUp.DTOs
{
    public class ExcursionCostSellingCreateDto
    {
        public int? PriceListId { get; set; }
        public int? ExcursionId { get; set; }
        public int? DestinationId { get; set; }
        public int? AgentId { get; set; }
        public int? SupplierId { get; set; }
        // Selling ADL
        [Range(0, double.MaxValue)] public decimal SellingAdlEGP { get; set; }
        [Range(0, double.MaxValue)] public decimal SellingAdlUSD { get; set; }
        [Range(0, double.MaxValue)] public decimal SellingAdlEUR { get; set; }
        [Range(0, double.MaxValue)] public decimal SellingAdlGBP { get; set; }
        // Selling CHD
        [Range(0, double.MaxValue)] public decimal SellingChdEGP { get; set; }
        [Range(0, double.MaxValue)] public decimal SellingChdUSD { get; set; }
        [Range(0, double.MaxValue)] public decimal SellingChdEUR { get; set; }
        [Range(0, double.MaxValue)] public decimal SellingChdGBP { get; set; }
        // Cost ADL
        [Range(0, double.MaxValue)] public decimal CostAdlEGP { get; set; }
        [Range(0, double.MaxValue)] public decimal CostAdlUSD { get; set; }
        [Range(0, double.MaxValue)] public decimal CostAdlEUR { get; set; }
        [Range(0, double.MaxValue)] public decimal CostAdlGBP { get; set; }
        // Cost CHD
        [Range(0, double.MaxValue)] public decimal CostChdEGP { get; set; }
        [Range(0, double.MaxValue)] public decimal CostChdUSD { get; set; }
        [Range(0, double.MaxValue)] public decimal CostChdEUR { get; set; }
        [Range(0, double.MaxValue)] public decimal CostChdGBP { get; set; }
        // National Fee ADL
        [Range(0, double.MaxValue)] public decimal NationalFeeAdlEGP { get; set; }
        [Range(0, double.MaxValue)] public decimal NationalFeeAdlUSD { get; set; }
        // National Fee CHD
        [Range(0, double.MaxValue)] public decimal NationalFeeChdEGP { get; set; }
        [Range(0, double.MaxValue)] public decimal NationalFeeChdUSD { get; set; }
        [MaxLength(100)] public string RecordBy { get; set; } = "System";
    }
}
