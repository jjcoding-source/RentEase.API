namespace RentEase.API.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int RenterId { get; set; }

        public string Status { get; set; } = "Pending"; // Pending | Confirmed | Rejected | Completed
        public DateTime MoveIn { get; set; }
        public DateTime? MoveOut { get; set; }
        public int Duration { get; set; } // months
        public string Occupants { get; set; } = "1 person";
        public string Parking { get; set; } = "No";
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Property Property { get; set; } = null!;
        public User Renter { get; set; } = null!;
    }
}
