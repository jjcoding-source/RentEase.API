namespace RentEase.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Renter"; 
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? DateOfBirth { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool PrefBookingUpdates { get; set; } = true;
        public bool PrefNewListings { get; set; } = true;
        public bool PrefPriceDropAlerts { get; set; } = false;
        public bool PrefMarketingEmails { get; set; } = false;

        public ICollection<Property> Properties { get; set; } = new List<Property>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
