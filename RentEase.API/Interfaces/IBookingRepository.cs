
using RentEase.API.Models;

namespace RentEase.API.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> CreateAsync(Booking booking);
        Task<List<Booking>> GetByRenterIdAsync(int renterId);
        Task<List<Booking>> GetByOwnerIdAsync(int ownerId);
        Task<List<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task<Booking?> UpdateStatusAsync(int id, string status);
    }
}