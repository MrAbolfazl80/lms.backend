using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Application.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories {
    public class EnrollmentRepository : BaseRepository<Enrollment>, IEnrollmentRepository {
        public EnrollmentRepository(LmsDbContext context) : base(context) {
        }

        public async Task<bool> ExistsAsync(int studentId, int courseId) {
            return await _dbSet.AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }

        public Task<List<Enrollment>> GetEnrollmentsByUserId(int studentId) {
            return _dbSet.Where(x => x.StudentId == studentId).ToListAsync();
        }
        public Task<int[]> GetEnrollmentsIdsByUserId(int studentId) {
            return _dbSet.Where(x => x.StudentId == studentId)
                .Select(x=>x.CourseId)
                .ToArrayAsync();
        }
    }
}
