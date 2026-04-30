using System.ComponentModel.DataAnnotations;

namespace RentEase.API.DTOs.Booking
{
    public class UpdateBookingStatusRequest
    {
        [Required]
        public string Status { get; set; } = string.Empty; // Confirmed | Rejected
    }
}