using Application.Repositories;
using Application.Utilities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAuthService {
        Task<string> LoginAsync(string username, string password);
        Task<string> RegisterAsync(string username, string password, string role);
    }
}
