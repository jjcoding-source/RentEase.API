namespace RentEase.API.DTOs.Property
{
    public class PropertyFilterParams
    {
        public string? Search { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Types { get; set; } 
        public string? Bedrooms { get; set; }
        public string? Amenities { get; set; } 
        public string? Sort { get; set; } // relevance | price_asc | price_desc | newest
        public bool Owner { get; set; } = false; 
    }
}