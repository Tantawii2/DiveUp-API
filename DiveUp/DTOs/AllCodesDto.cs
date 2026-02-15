namespace DiveUp.DTOs
{
    public class CodeItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class CodeCategoryDto
    {
        public string Key { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
    }

    public class AllCodesDto
    {
        public List<CodeItemDto> Agents { get; set; } = new();
        public List<CodeItemDto> Boats { get; set; } = new();
        public List<CodeItemDto> Excursions { get; set; } = new();
        public List<CodeItemDto> ExcursionSuppliers { get; set; } = new();
        public List<CodeItemDto> Guides { get; set; } = new();
        public List<CodeItemDto> Hotels { get; set; } = new();
        public List<CodeItemDto> HotelDestinations { get; set; } = new();
        public List<CodeItemDto> Nationalities { get; set; } = new();
        public List<CodeItemDto> PriceLists { get; set; } = new();
        public List<CodeItemDto> Rates { get; set; } = new();
        public List<CodeItemDto> Reps { get; set; } = new();
        public List<CodeItemDto> TransportationTypes { get; set; } = new();
        public List<CodeItemDto> TransportationSuppliers { get; set; } = new();
        public List<CodeItemDto> TransportationCosts { get; set; } = new();
        public List<CodeItemDto> Vouchers { get; set; } = new();
    }
}
