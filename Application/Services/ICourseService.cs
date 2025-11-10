using Application.Common;
using Application.DTOs.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICourseService {
        Task<PagedResult<CourseDto>> GetPagedAsync(int pageNumber, int pageSize);
        Task<PagedResult<UserEnrolledCourseListDto>> GetEnrolledCoursesAsync(int userId, int pageNumber, int pageSize);
        Task<PagedResult<AvailableCourseListDto>> GetAvailableCoursesAsync(int pageNumber, int pageSize, int? userId = null);
        Task<CourseDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateCourseRequest request);
        Task<bool> UpdateAsync(int id, UpdateCourseRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
