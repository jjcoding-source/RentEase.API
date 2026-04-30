
using RentEase.API.Data;
using RentEase.API.DTOs.Admin;
using RentEase.API.DTOs.Booking;
using RentEase.API.DTOs.Property;
using RentEase.API.DTOs.User;
using RentEase.API.Helpers;
using RentEase.API.Interfaces;

namespace RentEase.API.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPropertyRepository _propRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly AppDbContext _db;

        public AdminService(
            IUserRepository userRepo,
            IPropertyRepository propRepo,
            IBookingRepository bookingRepo,
            AppDbContext db)
        {
            _userRepo = userRepo;
            _propRepo = propRepo;
            _bookingRepo = bookingRepo;
            _db = db;
        }

        public async Task<AdminStatsResponse> GetStatsAsync()
        {
            var users = await _userRepo.GetAllAsync();
            var bookings = await _bookingRepo.GetAllAsync();
            var properties = await _propRepo.GetAllAsync(new DTOs.Property.PropertyFilterParams());

            var confirmedBookings = bookings.Where(b => b.Status == "Confirmed").ToList();
            var monthlyRevenue = confirmedBookings
                .Sum(b => b.Property?.Rent ?? 0);

            return new AdminStatsResponse
            {
                TotalUsers = users.Count,
                TotalOwners = users.Count(u => u.Role == "Owner"),
                TotalRenters = users.Count(u => u.Role == "Renter"),
                TotalProperties = properties.Count,
                PendingProperties = properties.Count(p => p.Status == "Pending"),
                ActiveBookings = confirmedBookings.Count,
                MonthlyRevenue = monthlyRevenue,
            };
        }

        public async Task<List<UserResponse>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return users.Select(MappingHelper.ToUserResponse).ToList();
        }

        public async Task<List<PropertyResponse>> GetAllPropertiesAsync()
        {
            var props = await _propRepo.GetAllAsync(new DTOs.Property.PropertyFilterParams());
            return props.Select(MappingHelper.ToPropertyResponse).ToList();
        }

        public async Task<List<BookingResponse>> GetAllBookingsAsync()
        {
            var bookings = await _bookingRepo.GetAllAsync();
            return bookings.Select(MappingHelper.ToBookingResponse).ToList();
        }

        public async Task<bool> ApprovePropertyAsync(int propertyId)
        {
            var prop = await _propRepo.GetByIdAsync(propertyId);
            if (prop == null) return false;
            prop.Status = "Active";
            await _propRepo.UpdateAsync(prop);
            return true;
        }

        public async Task<bool> RejectPropertyAsync(int propertyId)
        {
            var prop = await _propRepo.GetByIdAsync(propertyId);
            if (prop == null) return false;
            prop.Status = "Rejected";
            await _propRepo.UpdateAsync(prop);
            return true;
        }

        public async Task<bool> SetUserActiveAsync(int userId, bool isActive)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) return false;
            user.IsActive = isActive;
            await _userRepo.UpdateAsync(user);
            return true;
        }
    }
}