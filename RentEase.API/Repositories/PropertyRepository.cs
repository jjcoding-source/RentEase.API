using Microsoft.EntityFrameworkCore;
using RentEase.API.Data;
using RentEase.API.DTOs.Property;
using RentEase.API.Interfaces;
using RentEase.API.Models;

namespace RentEase.API.Repositories
{
    // Saved properties
    public class SavedProperty
    {
        public int UserId { get; set; }
        public int PropertyId { get; set; }
    }

    public class PropertyRepository : IPropertyRepository
    {
        private readonly AppDbContext _db;
        public PropertyRepository(AppDbContext db) { _db = db; }

        public async Task<List<Property>> GetAllAsync(PropertyFilterParams filters, int? ownerId = null)
        {
            var query = _db.Properties
                .Include(p => p.Owner)
                .AsQueryable();

            // Owner filter — only show their own properties
            if (ownerId.HasValue)
                query = query.Where(p => p.OwnerId == ownerId.Value);
            else
                query = query.Where(p => p.Status == "Active");

            // Search
            if (!string.IsNullOrWhiteSpace(filters.Search))
                query = query.Where(p =>
                    p.Title.Contains(filters.Search) ||
                    p.City.Contains(filters.Search) ||
                    p.Address.Contains(filters.Search));

            // Price range
            if (filters.MinPrice.HasValue)
                query = query.Where(p => p.Rent >= filters.MinPrice.Value);
            if (filters.MaxPrice.HasValue)
                query = query.Where(p => p.Rent <= filters.MaxPrice.Value);

            // Types 
            if (!string.IsNullOrWhiteSpace(filters.Types))
            {
                var types = filters.Types.Split(',').Select(t => t.Trim()).ToList();
                query = query.Where(p => types.Contains(p.Type));
            }

            // Bedrooms
            if (!string.IsNullOrWhiteSpace(filters.Bedrooms) && filters.Bedrooms != "Any")
            {
                if (filters.Bedrooms == "3+")
                    query = query.Where(p => p.Bedrooms >= 3);
                else if (int.TryParse(filters.Bedrooms, out var beds))
                    query = query.Where(p => p.Bedrooms == beds);
            }

            // Sort
            query = filters.Sort switch
            {
                "price_asc" => query.OrderBy(p => p.Rent),
                "price_desc" => query.OrderByDescending(p => p.Rent),
                "newest" => query.OrderByDescending(p => p.CreatedAt),
                _ => query.OrderByDescending(p => p.Featured)
                                     .ThenByDescending(p => p.Rating),
            };

            return await query.ToListAsync();
        }

        public Task<Property?> GetByIdAsync(int id) =>
            _db.Properties
               .Include(p => p.Owner)
               .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Property> CreateAsync(Property property)
        {
            _db.Properties.Add(property);
            await _db.SaveChangesAsync();
            return property;
        }

        public async Task<Property?> UpdateAsync(Property property)
        {
            property.UpdatedAt = DateTime.UtcNow;
            _db.Properties.Update(property);
            await _db.SaveChangesAsync();
            return property;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var prop = await _db.Properties.FindAsync(id);
            if (prop == null) return false;
            _db.Properties.Remove(prop);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsSavedAsync(int propertyId, int userId)
        {
            return await _db.Set<SavedProperty>()
                .AnyAsync(s => s.PropertyId == propertyId && s.UserId == userId);
        }

        public async Task SavePropertyAsync(int propertyId, int userId)
        {
            var exists = await IsSavedAsync(propertyId, userId);
            if (!exists)
            {
                _db.Set<SavedProperty>().Add(new SavedProperty { PropertyId = propertyId, UserId = userId });
                await _db.SaveChangesAsync();
            }
        }

        public async Task UnsavePropertyAsync(int propertyId, int userId)
        {
            var saved = await _db.Set<SavedProperty>()
                .FirstOrDefaultAsync(s => s.PropertyId == propertyId && s.UserId == userId);
            if (saved != null)
            {
                _db.Set<SavedProperty>().Remove(saved);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<Property>> GetSavedByUserAsync(int userId)
        {
            var savedIds = await _db.Set<SavedProperty>()
                .Where(s => s.UserId == userId)
                .Select(s => s.PropertyId)
                .ToListAsync();

            return await _db.Properties
                .Include(p => p.Owner)
                .Where(p => savedIds.Contains(p.Id))
                .ToListAsync();
        }
    }
}