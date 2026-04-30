
using RentEase.API.DTOs.Auth;

namespace RentEase.API.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> SignupAsync(SignupRequest request);
    }
}