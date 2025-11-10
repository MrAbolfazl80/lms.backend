using Application.Common;
using Application.Repositories;
using Application.Services;
using Application.Utilities;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IOptions<JwtSettings> jwtSettings, IUserRepository userRepository) {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<string> LoginAsync(string username, string password) {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null || !PasswordHelper.VerifyPassword(password, user.PasswordHash))
                throw new Exception("نام کاربری یا کلمه عبور اشتباه است.");

            return GenerateJwtToken(user);
        }

        public async Task<string> RegisterAsync(string username, string password, string role) {
            var existingUser = await _userRepository.GetByUsernameAsync(username);
            if (existingUser != null)
                throw new Exception("Username already exists");

            var hashedPassword = PasswordHelper.HashPassword(password);
            var user = new User(username, hashedPassword, role);

            await _userRepository.AddAsync(user);
            return "User registered successfully";
        }
        #region HelperMethods
        private string GenerateJwtToken(User user) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.Key);

            var now = DateTime.UtcNow;

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        }),
                NotBefore = now,
                Expires = now.AddMinutes(_jwtSettings.ExpireMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #endregion
    }

}
