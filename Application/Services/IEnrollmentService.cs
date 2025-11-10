using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IEnrollmentService {
        Task<bool> EnrollAsync(int userId, int courseId);
        Task<int[]> GetEnrolledCoursedIdsByUserIdAsync(int userId);
    }
}
