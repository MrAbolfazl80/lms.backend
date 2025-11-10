using Application.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository:BaseRepository<User>,IUserRepository
    {
        public UserRepository(LmsDbContext context):base(context) {
            
        }
        public async Task<User> GetByUsernameAsync(string username) {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
