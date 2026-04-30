
using RentEase.API.DTOs.User;
using RentEase.API.Helpers;
using RentEase.API.Interfaces;

namespace RentEase.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) { _repo = repo; }

        public async Task<UserResponse?> GetByIdAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            return user == null ? null : MappingHelper.ToUserResponse(user);
        }

        public async Task<UserResponse?> UpdateAsync(int id, UpdateUserRequest request)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return null;

            if (!string.IsNullOrWhiteSpace(request.Name)) user.Name = request.Name;
            if (!string.IsNullOrWhiteSpace(request.Email)) user.Email = request.Email;
            if (request.Phone != null) user.Phone = request.Phone;
            if (request.City != null) user.City = request.City;
            if (request.DateOfBirth != null) user.DateOfBirth = request.DateOfBirth;

            var updated = await _repo.UpdateAsync(user);
            return updated == null ? null : MappingHelper.ToUserResponse(updated);
        }

        public async Task<bool> ChangePasswordAsync(int id, ChangePasswordRequest request)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;

            if (!BCrypt.Net.BCrypt.Verify(request.CurrentPassword, user.PasswordHash))
                throw new UnauthorizedAccessException("Current password is incorrect.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await _repo.UpdateAsync(user);
            return true;
        }

        public async Task<bool> UpdatePreferencesAsync(int id, UpdatePreferencesRequest request)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null) return false;

            user.PrefBookingUpdates = request.PrefBookingUpdates;
            user.PrefNewListings = request.PrefNewListings;
            user.PrefPriceDropAlerts = request.PrefPriceDropAlerts;
            user.PrefMarketingEmails = request.PrefMarketingEmails;

            await _repo.UpdateAsync(user);
            return true;
        }

        public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}