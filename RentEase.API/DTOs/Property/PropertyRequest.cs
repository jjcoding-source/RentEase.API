using System.ComponentModel.DataAnnotations;

namespace RentEase.API.DTOs.Property
{
    public class PropertyRequest
    {
        [Required] public string Title { get; set; } = string.Empty;
        [Required] public string Type { get; set; } = string.Empty;
        public string Status { get; set; } = "Draft";
        [Required] public string Description { get; set; } = string.Empty;
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Area { get; set; }
        public int? Floor { get; set; }
        [Required] public string Address { get; set; } = string.Empty;
        [Required] public string City { get; set; } = string.Empty;
        [Required] public string State { get; set; } = string.Empty;
        public string Pincode { get; set; } = string.Empty;
        public string Country { get; set; } = "India";
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        [Required]
        [Range(1, double.MaxValue)]
        public decimal Rent { get; set; }
        public decimal Deposit { get; set; }
        public int MinLease { get; set; } = 11;
        public DateTime? AvailableFrom { get; set; }
        public List<string> Amenities { get; set; } = new();
        public List<string> Images { get; set; } = new();
    }
}