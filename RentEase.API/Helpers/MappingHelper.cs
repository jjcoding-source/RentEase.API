
using RentEase.API.DTOs.Booking;
using RentEase.API.DTOs.Property;
using RentEase.API.DTOs.User;
using RentEase.API.Models;

namespace RentEase.API.Helpers
{
    public static class MappingHelper
    {
        public static PropertyResponse ToPropertyResponse(Property p)
        {
            return new PropertyResponse
            {
                Id = p.Id,
                OwnerId = p.OwnerId,
                OwnerName = p.Owner?.Name ?? string.Empty,
                Title = p.Title,
                Type = p.Type,
                Status = p.Status,
                Description = p.Description,
                Bedrooms = p.Bedrooms,
                Bathrooms = p.Bathrooms,
                Area = p.Area,
                Floor = p.Floor,
                Featured = p.Featured,
                Address = p.Address,
                City = p.City,
                State = p.State,
                Pincode = p.Pincode,
                Country = p.Country,
                Latitude = p.Latitude,
                Longitude = p.Longitude,
                Rent = p.Rent,
                Deposit = p.Deposit,
                MinLease = p.MinLease,
                AvailableFrom = p.AvailableFrom,
                Amenities = string.IsNullOrEmpty(p.Amenities)
                                    ? new List<string>()
                                    : p.Amenities.Split(',').ToList(),
                Images = string.IsNullOrEmpty(p.Images)
                                    ? new List<string>()
                                    : p.Images.Split(',').ToList(),
                Rating = p.Rating,
                ReviewCount = p.ReviewCount,
                CreatedAt = p.CreatedAt,
            };
        }

        public static BookingResponse ToBookingResponse(Booking b)
        {
            return new BookingResponse
            {
                Id = b.Id,
                PropertyId = b.PropertyId,
                PropertyTitle = b.Property?.Title ?? string.Empty,
                City = b.Property?.City ?? string.Empty,
                Rent = b.Property?.Rent ?? 0,
                RenterId = b.RenterId,
                RenterName = b.Renter?.Name ?? string.Empty,
                RenterEmail = b.Renter?.Email ?? string.Empty,
                OwnerName = b.Property?.Owner?.Name ?? string.Empty,
                Status = b.Status,
                MoveIn = b.MoveIn,
                MoveOut = b.MoveOut,
                Duration = b.Duration,
                Occupants = b.Occupants,
                Parking = b.Parking,
                Message = b.Message,
                CreatedAt = b.CreatedAt,
            };
        }

        public static UserResponse ToUserResponse(User u)
        {
            return new UserResponse
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role,
                Phone = u.Phone,
                City = u.City,
                DateOfBirth = u.DateOfBirth,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt,
                PrefBookingUpdates = u.PrefBookingUpdates,
                PrefNewListings = u.PrefNewListings,
                PrefPriceDropAlerts = u.PrefPriceDropAlerts,
                PrefMarketingEmails = u.PrefMarketingEmails,
            };
        }
    }
}