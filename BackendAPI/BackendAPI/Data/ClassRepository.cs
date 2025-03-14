using BackendAPI.DTOs;
using BackendAPI.Interfaces;
using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Data
{
    public class ClassRepository : BaseRepository<Class>, IClassRepository
    {
        public ClassRepository(AppDbContext context) : base(context) { }

        public async Task<bool> JoinClass(EnrollmentDTO enrollment)
        {
            var user = await _context.Users.FindAsync(enrollment.UserId);
            var @class = await _context.Classes.FindAsync(enrollment.ClassId);

            if(user == null || @class == null) { return false; }

            if(await _context.Enrollments
                .AnyAsync(uc => uc.UserId == enrollment.UserId && uc.ClassId == enrollment.ClassId))
            {
                return false;
            }

            var newEnrollment = new Enrollment
            {
                UserId = enrollment.UserId,
                ClassId = enrollment.ClassId
            };

            await _context.Enrollments.AddAsync(newEnrollment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
