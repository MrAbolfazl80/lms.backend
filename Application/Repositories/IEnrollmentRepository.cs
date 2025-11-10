using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Repositories {
    public interface IEnrollmentRepository : IBaseRepository<Enrollment> {
        Task<bool> ExistsAsync(int studentId, int courseId);
        Task<List<Enrollment>> GetEnrollmentsByUserId(int studentId);
        Task<int[]> GetEnrollmentsIdsByUserId(int studentId);
    }
}
