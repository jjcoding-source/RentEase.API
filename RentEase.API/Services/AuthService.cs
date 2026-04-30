
using RentEase.API.DTOs.Auth;
using RentEase.API.Helpers;
using RentEase.API.Interfaces;
using RentEase.API.Models;

namespace RentEase.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly JwtHelper _jwt;

        public AuthService(IUserRepository userRepo, JwtHelper jwt)
        {
            _userRepo = userRepo;
            _jwt = jwt;
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepo.GetByEmailAsync(request.Email)
                ?? throw new UnauthorizedAccessException("Invalid email or password.");

            if (!user.IsActive)
                throw new UnauthorizedAccessException("Account is suspended.");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password.");

            return new AuthResponse
            {
                Token = _jwt.GenerateToken(user),
                Role = user.Role,
                Name = user.Name,
            };
        }

        public async Task<AuthResponse> SignupAsync(SignupRequest request)
        {
            var existing = await _userRepo.GetByEmailAsync(request.Email);
            if (existing != null)
                throw new InvalidOperationException("An account with this email already exists.");

   
            var role = request.Role switch
            {
                "Owner" => "Owner",
                "Admin" => "Admin",   
                _ => "Renter",
            };

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = role,
            };

            var created = await _userRepo.CreateAsync(user);

            return new AuthResponse
            {
                Token = _jwt.GenerateToken(created),
                Role = created.Role,
                Name = created.Name,
            };
        }
    }
}