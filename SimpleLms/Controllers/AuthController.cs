using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SimpleLms.Controllers.Base;

namespace SimpleLms.Controllers {

    public class AuthController : LmsControllerBase {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AuthRegisterRequestDto request) {
            var result = await _authService.RegisterAsync(request.Username, request.Password, request.Role);
            return Success(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthLoginRequestDto request) {
            var token = await _authService.LoginAsync(request.Username, request.Password);
            return Success(token);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UserRoles() {
            var roles = await _authService.GetUserRolesForDropDownAsync();
            return Success(roles);
        }
    }

}
