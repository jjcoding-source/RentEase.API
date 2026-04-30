namespace RentEase.API.DTOs.User
{
    public class UpdateUserRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? DateOfBirth { get; set; }
    }
}