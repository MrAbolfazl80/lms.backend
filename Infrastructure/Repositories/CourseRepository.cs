using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories {
    public class CourseRepository : BaseRepository<Course>, ICourseRepository {
        public CourseRepository(LmsDbContext context) : base(context) {
        }

        public async Task<IEnumerable<Course>> GetAllWithEnrollmentsAsync() {
            return await _dbSet.Include(c => c.Enrollments).ToListAsync();
        }

        public async Task<Course> GetByIdWithEnrollmentsAsync(int id) {
            return await _dbSet
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<(IEnumerable<Course> Courses, int TotalCount)> GetAvailablePagedAsync(int pageNumber, int pageSize, DateTime now,int? studentId=null) {
            var query = _dbSet
                .Include(c => c.Enrollments)
                .Where(c =>(studentId==null||c.Enrollments.Any(x=>x.StudentId==studentId)));

            var totalCount = await query.CountAsync();

            var courses = await query
                .OrderByDescending(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (courses, totalCount);
        }

    }
}
