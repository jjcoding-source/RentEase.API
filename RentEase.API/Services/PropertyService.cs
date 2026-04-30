using RentEase.API.DTOs.Property;
using RentEase.API.Helpers;
using RentEase.API.Interfaces;
using RentEase.API.Models;

namespace RentEase.API.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _repo;
        public PropertyService(IPropertyRepository repo) { _repo = repo; }

        public async Task<List<PropertyResponse>> GetAllAsync(PropertyFilterParams filters, int? requestingUserId)
        {
            var ownerId = filters.Owner ? requestingUserId : null;
            var props = await _repo.GetAllAsync(filters, ownerId);
            return props.Select(MappingHelper.ToPropertyResponse).ToList();
        }

        public async Task<PropertyResponse?> GetByIdAsync(int id)
        {
            var prop = await _repo.GetByIdAsync(id);
            return prop == null ? null : MappingHelper.ToPropertyResponse(prop);
        }

        public async Task<PropertyResponse> CreateAsync(PropertyRequest request, int ownerId)
        {
            var property = MapFromRequest(request, ownerId);
            
            property.Status = request.Status == "Draft" ? "Draft" : "Pending";
            var created = await _repo.CreateAsync(property);
           
            var full = await _repo.GetByIdAsync(created.Id);
            return MappingHelper.ToPropertyResponse(full!);
        }

        public async Task<PropertyResponse?> UpdateAsync(int id, PropertyRequest request, int ownerId)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null || existing.OwnerId != ownerId) return null;

            existing.Title = request.Title;
            existing.Type = request.Type;
            existing.Description = request.Description;
            existing.Bedrooms = request.Bedrooms;
            existing.Bathrooms = request.Bathrooms;
            existing.Area = request.Area;
            existing.Floor = request.Floor;
            existing.Address = request.Address;
            existing.City = request.City;
            existing.State = request.State;
            existing.Pincode = request.Pincode;
            existing.Country = request.Country;
            existing.Latitude = request.Latitude;
            existing.Longitude = request.Longitude;
            existing.Rent = request.Rent;
            existing.Deposit = request.Deposit;
            existing.MinLease = request.MinLease;
            existing.AvailableFrom = request.AvailableFrom;
            existing.Amenities = string.Join(',', request.Amenities);
            existing.Images = string.Join(',', request.Images);
            existing.Status = request.Status;

            var updated = await _repo.UpdateAsync(existing);
            return updated == null ? null : MappingHelper.ToPropertyResponse(updated);
        }

        public async Task<bool> DeleteAsync(int id, int ownerId)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null || existing.OwnerId != ownerId) return false;
            return await _repo.DeleteAsync(id);
        }

        public async Task<List<PropertyResponse>> GetSavedAsync(int userId)
        {
            var saved = await _repo.GetSavedByUserAsync(userId);
            return saved.Select(MappingHelper.ToPropertyResponse).ToList();
        }

        public async Task<bool> ToggleSaveAsync(int propertyId, int userId)
        {
            if (await _repo.IsSavedAsync(propertyId, userId))
            {
                await _repo.UnsavePropertyAsync(propertyId, userId);
                return false; // unsaved
            }
            await _repo.SavePropertyAsync(propertyId, userId);
            return true; // saved
        }

        private static Property MapFromRequest(PropertyRequest r, int ownerId) => new()
        {
            OwnerId = ownerId,
            Title = r.Title,
            Type = r.Type,
            Description = r.Description,
            Bedrooms = r.Bedrooms,
            Bathrooms = r.Bathrooms,
            Area = r.Area,
            Floor = r.Floor,
            Address = r.Address,
            City = r.City,
            State = r.State,
            Pincode = r.Pincode,
            Country = r.Country,
            Latitude = r.Latitude,
            Longitude = r.Longitude,
            Rent = r.Rent,
            Deposit = r.Deposit,
            MinLease = r.MinLease,
            AvailableFrom = r.AvailableFrom,
            Amenities = string.Join(',', r.Amenities),
            Images = string.Join(',', r.Images),
        };
    }
}