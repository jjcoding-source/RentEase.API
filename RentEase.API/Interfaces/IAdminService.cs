
using RentEase.API.DTOs.Admin;
using RentEase.API.DTOs.Booking;
using RentEase.API.DTOs.Property;
using RentEase.API.DTOs.User;

namespace RentEase.API.Interfaces
{
    public interface IAdminService
    {
        Task<AdminStatsResponse> GetStatsAsync();
        Task<List<UserResponse>> GetAllUsersAsync();
        Task<List<PropertyResponse>> GetAllPropertiesAsync();
        Task<List<BookingResponse>> GetAllBookingsAsync();
        Task<bool> ApprovePropertyAsync(int propertyId);
        Task<bool> RejectPropertyAsync(int propertyId);
        Task<bool> SetUserActiveAsync(int userId, bool isActive);
    }
}