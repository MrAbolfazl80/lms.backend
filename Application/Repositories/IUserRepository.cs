using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Repositories {
    public interface IUserRepository : IBaseRepository<User> {
        Task<User> GetByUsernameAsync(string username);
    }
}
