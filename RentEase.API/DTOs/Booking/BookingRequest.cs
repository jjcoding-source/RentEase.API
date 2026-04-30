using System.ComponentModel.DataAnnotations;

namespace RentEase.API.DTOs.Booking
{
    public class BookingRequest
    {
        [Required] public int PropertyId { get; set; }
        [Required] public DateTime MoveIn { get; set; }
        public DateTime? MoveOut { get; set; }
        [Required] public int Duration { get; set; }
        public string Occupants { get; set; } = "1 person";
        public string Parking { get; set; } = "No";
        public string? Message { get; set; }
    }
}