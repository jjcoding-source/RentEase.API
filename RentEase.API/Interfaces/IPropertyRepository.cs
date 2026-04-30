
using RentEase.API.DTOs.Property;
using RentEase.API.Models;

namespace RentEase.API.Interfaces
{
    public interface IPropertyRepository
    {
        Task<List<Property>> GetAllAsync(PropertyFilterParams filters, int? ownerId = null);
        Task<Property?> GetByIdAsync(int id);
        Task<Property> CreateAsync(Property property);
        Task<Property?> UpdateAsync(Property property);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsSavedAsync(int propertyId, int userId);
        Task SavePropertyAsync(int propertyId, int userId);
        Task UnsavePropertyAsync(int propertyId, int userId);
        Task<List<Property>> GetSavedByUserAsync(int userId);
    }
}