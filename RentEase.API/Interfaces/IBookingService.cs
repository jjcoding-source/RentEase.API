
using RentEase.API.DTOs.Booking;

namespace RentEase.API.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResponse> CreateAsync(BookingRequest request, int renterId);
        Task<List<BookingResponse>> GetMyBookingsAsync(int renterId);
        Task<List<BookingResponse>> GetOwnerBookingsAsync(int ownerId);
        Task<List<BookingResponse>> GetHistoryAsync(int renterId);
        Task<List<BookingResponse>> GetOwnerHistoryAsync(int ownerId);
        Task<BookingResponse?> UpdateStatusAsync(int id, string status, int ownerId);
    }
}