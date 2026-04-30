namespace RentEase.API.DTOs.Booking
{
    public class BookingResponse
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public string PropertyTitle { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public decimal Rent { get; set; }
        public int RenterId { get; set; }
        public string RenterName { get; set; } = string.Empty;
        public string RenterEmail { get; set; } = string.Empty;
        public double? RenterScore { get; set; }
        public string OwnerName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime MoveIn { get; set; }
        public DateTime? MoveOut { get; set; }
        public int Duration { get; set; }
        public string Occupants { get; set; } = string.Empty;
        public string Parking { get; set; } = string.Empty;
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}