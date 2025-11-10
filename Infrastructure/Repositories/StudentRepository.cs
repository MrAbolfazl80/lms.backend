using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Application.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories {
    public class StudentRepository : BaseRepository<Student>, IStudentRepository {
        public StudentRepository(LmsDbContext context) : base(context) {
        }


        public async Task<Student> GetByIdWithEnrollmentsAsync(int id) {
            return await _dbSet
                .Include(s => s.Enrollments)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Student> GetStudentByUserIdAsync(int userId) {
            return await _dbSet
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
        public async Task<int?> GetStudentIdByUserIdAsync(int userId) {
            var student= await _dbSet
                .FirstOrDefaultAsync(x => x.UserId == userId);
            return student?.Id;
        }
    }
}
