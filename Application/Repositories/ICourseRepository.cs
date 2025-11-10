using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories {
    public interface ICourseRepository : IBaseRepository<Course> {
        Task<IEnumerable<Course>> GetAllWithEnrollmentsAsync();
        Task<Course> GetByIdWithEnrollmentsAsync(int id);
        Task<(IEnumerable<Course> Courses, int TotalCount)> GetAvailablePagedAsync(int pageNumber, int pageSize, DateTime now,int? studentId = null);
    }
}
