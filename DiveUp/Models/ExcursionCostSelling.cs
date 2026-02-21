using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DiveUp.Models
{
    public class ExcursionCostSelling
    {
        public int Id { get; set; }

        // Lookups
        public int? PriceListId { get; set; }
        public PriceList? PriceList { get; set; }

        public int? ExcursionId { get; set; }
        public Excursion? Excursion { get; set; }

        public int? DestinationId { get; set; }
        public HotelDestination? Destination { get; set; }

        public int? AgentId { get; set; }
        public Agent? Agent { get; set; }

        public int? SupplierId { get; set; }
        public ExcursionSupplier? Supplier { get; set; }

        // Selling ADL
        [Column(TypeName = "decimal(18,2)")] public decimal SellingAdlEGP { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal SellingAdlUSD { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal SellingAdlEUR { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal SellingAdlGBP { get; set; }

        // Selling CHD
        [Column(TypeName = "decimal(18,2)")] public decimal SellingChdEGP { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal SellingChdUSD { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal SellingChdEUR { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal SellingChdGBP { get; set; }

        // Cost ADL
        [Column(TypeName = "decimal(18,2)")] public decimal CostAdlEGP { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal CostAdlUSD { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal CostAdlEUR { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal CostAdlGBP { get; set; }

        // Cost CHD
        [Column(TypeName = "decimal(18,2)")] public decimal CostChdEGP { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal CostChdUSD { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal CostChdEUR { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal CostChdGBP { get; set; }

        // National Fee ADL
        [Column(TypeName = "decimal(18,2)")] public decimal NationalFeeAdlEGP { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal NationalFeeAdlUSD { get; set; }

        // National Fee CHD
        [Column(TypeName = "decimal(18,2)")] public decimal NationalFeeChdEGP { get; set; }
        [Column(TypeName = "decimal(18,2)")] public decimal NationalFeeChdUSD { get; set; }

        [MaxLength(100)] public string RecordBy { get; set; } = "System";
        public DateTime RecordTime { get; set; } = DateTime.UtcNow;
    }
}
