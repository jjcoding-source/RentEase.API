namespace RentEase.API.DTOs.User
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool PrefBookingUpdates { get; set; }
        public bool PrefNewListings { get; set; }
        public bool PrefPriceDropAlerts { get; set; }
        public bool PrefMarketingEmails { get; set; }
    }
}