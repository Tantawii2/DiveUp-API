namespace DiveUp.DTOs
{
    public class ExcursionCostSellingDto
    {
        public int Id { get; set; }
        public int? PriceListId { get; set; }    public string? PriceListName { get; set; }
        public int? ExcursionId { get; set; }    public string? ExcursionName { get; set; }
        public int? DestinationId { get; set; }  public string? DestinationName { get; set; }
        public int? AgentId { get; set; }        public string? AgentName { get; set; }
        public int? SupplierId { get; set; }     public string? SupplierName { get; set; }
        // Selling
        public decimal SellingAdlEGP { get; set; } public decimal SellingAdlUSD { get; set; }
        public decimal SellingAdlEUR { get; set; } public decimal SellingAdlGBP { get; set; }
        public decimal SellingChdEGP { get; set; } public decimal SellingChdUSD { get; set; }
        public decimal SellingChdEUR { get; set; } public decimal SellingChdGBP { get; set; }
        // Cost
        public decimal CostAdlEGP { get; set; } public decimal CostAdlUSD { get; set; }
        public decimal CostAdlEUR { get; set; } public decimal CostAdlGBP { get; set; }
        public decimal CostChdEGP { get; set; } public decimal CostChdUSD { get; set; }
        public decimal CostChdEUR { get; set; } public decimal CostChdGBP { get; set; }
        // National Fee
        public decimal NationalFeeAdlEGP { get; set; } public decimal NationalFeeAdlUSD { get; set; }
        public decimal NationalFeeChdEGP { get; set; } public decimal NationalFeeChdUSD { get; set; }
        public string RecordBy { get; set; } = string.Empty;
        public DateTime RecordTime { get; set; }
    }
}
