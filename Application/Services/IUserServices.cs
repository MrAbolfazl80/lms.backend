using Application.Common;
using Application.DTOs.Users;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserServices
    {
        Task<PagedResult<UserDto>> GetAllUsersAsync(GetUsersRequestDto request, int pageNumber, int pageSize);
    }
}
