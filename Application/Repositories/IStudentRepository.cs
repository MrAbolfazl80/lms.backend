using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IStudentRepository:IBaseRepository<Student>
    {
        Task<Student> GetByIdWithEnrollmentsAsync(int id);
        Task<Student> GetStudentByUserIdAsync(int userId);
        Task<int?> GetStudentIdByUserIdAsync(int userId);
    }
}
