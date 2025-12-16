using Application.Common;
using Application.DTOs.Users;
using Application.Repositories;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services {
    public class UserServices : IUserServices {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository) {
            _userRepository = userRepository;
        }
        public async Task<PagedResult<UserDto>> GetAllUsersAsync(GetUsersRequestDto request, int pageNumber, int pageSize) {
            var query = _userRepository
                .Query
                .Where(x =>
                (string.IsNullOrEmpty(request.UserName) || x.Username.ToLower().Contains(request.UserName.ToLower())) &&
                (string.IsNullOrEmpty(request.Role) || x.Role.ToLower().Contains(request.Role.ToLower()))
                );
            var totalCount = await query.CountAsync();
            var result = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new UserDto {
                    Id = r.Id,
                    Role = r.Role,
                    UserName = r.Username
                })
                .ToListAsync();
            return new PagedResult<UserDto>(result,totalCount,pageNumber,pageSize);
        }
    }
}
