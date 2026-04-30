
using RentEase.API.DTOs.Property;

namespace RentEase.API.Interfaces
{
    public interface IPropertyService
    {
        Task<List<PropertyResponse>> GetAllAsync(PropertyFilterParams filters, int? requestingUserId);
        Task<PropertyResponse?> GetByIdAsync(int id);
        Task<PropertyResponse> CreateAsync(PropertyRequest request, int ownerId);
        Task<PropertyResponse?> UpdateAsync(int id, PropertyRequest request, int ownerId);
        Task<bool> DeleteAsync(int id, int ownerId);
        Task<List<PropertyResponse>> GetSavedAsync(int userId);
        Task<bool> ToggleSaveAsync(int propertyId, int userId);
    }
}