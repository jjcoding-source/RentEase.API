
using Microsoft.EntityFrameworkCore;
using RentEase.API.Data;
using RentEase.API.Interfaces;
using RentEase.API.Models;

namespace RentEase.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) { _db = db; }

        public Task<User?> GetByIdAsync(int id) =>
            _db.Users.FirstOrDefaultAsync(u => u.Id == id);

        public Task<User?> GetByEmailAsync(string email) =>
            _db.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<User> CreateAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateAsync(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return false;
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return true;
        }

        public Task<List<User>> GetAllAsync() =>
            _db.Users.OrderByDescending(u => u.CreatedAt).ToListAsync();
    }
}