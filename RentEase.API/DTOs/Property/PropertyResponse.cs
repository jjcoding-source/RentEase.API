namespace RentEase.API.DTOs.Property
{
    public class PropertyResponse
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Area { get; set; }
        public int? Floor { get; set; }
        public bool Featured { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Pincode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public decimal Rent { get; set; }
        public decimal Deposit { get; set; }
        public int MinLease { get; set; }
        public DateTime? AvailableFrom { get; set; }
        public List<string> Amenities { get; set; } = new();
        public List<string> Images { get; set; } = new();
        public double? Rating { get; set; }
        public int ReviewCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}