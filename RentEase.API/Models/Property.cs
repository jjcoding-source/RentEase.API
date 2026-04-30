namespace RentEase.API.Models
{
    public class Property
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }

        // Basic info
        public string Title { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Apartment | House | Studio | Villa | PG
        public string Status { get; set; } = "Draft";      // Draft | Pending | Active | Inactive | Rejected
        public string Description { get; set; } = string.Empty;
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Area { get; set; } // sqft
        public int? Floor { get; set; }
        public bool Featured { get; set; } = false;

        // Location
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Pincode { get; set; } = string.Empty;
        public string Country { get; set; } = "India";
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        // Pricing
        public decimal Rent { get; set; }
        public decimal Deposit { get; set; }
        public int MinLease { get; set; } = 11; 
        public DateTime? AvailableFrom { get; set; }

        public string Amenities { get; set; } = string.Empty;

        public string Images { get; set; } = string.Empty;

        public double? Rating { get; set; }
        public int ReviewCount { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public User Owner { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
