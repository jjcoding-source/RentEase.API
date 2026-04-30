
using Microsoft.EntityFrameworkCore;
using RentEase.API.Data;
using RentEase.API.Interfaces;
using RentEase.API.Models;

namespace RentEase.API.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _db;
        public BookingRepository(AppDbContext db) { _db = db; }

        public async Task<Booking> CreateAsync(Booking booking)
        {
            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();
            return booking;
        }

        public Task<List<Booking>> GetByRenterIdAsync(int renterId) =>
            _db.Bookings
               .Include(b => b.Property)
               .Where(b => b.RenterId == renterId)
               .ToListAsync();

        public Task<List<Booking>> GetByOwnerIdAsync(int ownerId) =>
            _db.Bookings
               .Include(b => b.Property)
               .Where(b => b.Property.OwnerId == ownerId)
               .ToListAsync();

        public Task<List<Booking>> GetAllAsync() =>
            _db.Bookings
               .Include(b => b.Property)
               .OrderByDescending(b => b.CreatedAt)
               .ToListAsync();

        public Task<Booking?> GetByIdAsync(int id) =>
            _db.Bookings
               .Include(b => b.Property)
               .FirstOrDefaultAsync(b => b.Id == id);

        public async Task<Booking?> UpdateStatusAsync(int id, string status)
        {
            var booking = await _db.Bookings.FindAsync(id);
            if (booking == null) return null;
            booking.Status = status;
            _db.Bookings.Update(booking);
            await _db.SaveChangesAsync();
            return booking;
        }
    }
}
