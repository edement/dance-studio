using BackendAPI.Interfaces;
using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Data
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }
        public async Task<User?> GetByLoginAsync(string login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login) ?? throw new Exception(); //error
            return user;
        }
        public async Task<List<Class>> GetMyEnrollmentsAsync(Guid userId)
        {
            var enrollments = await _context.Enrollments
                .Where(e => e.UserId == userId)
                .Select(e => e.Class)
                .ToListAsync();
            return enrollments;
        }
    }
}
