
using RentEase.API.DTOs.User;

namespace RentEase.API.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse?> GetByIdAsync(int id);
        Task<UserResponse?> UpdateAsync(int id, UpdateUserRequest request);
        Task<bool> ChangePasswordAsync(int id, ChangePasswordRequest request);
        Task<bool> UpdatePreferencesAsync(int id, UpdatePreferencesRequest request);
        Task<bool> DeleteAsync(int id);
    }
}