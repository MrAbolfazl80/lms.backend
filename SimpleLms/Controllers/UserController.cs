using Application.DTOs.Users;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleLms.Controllers.Base;

namespace SimpleLms.Controllers
{

    [ApiController]
    public class UserController : LmsControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices) {
            _userServices = userServices;
        }
        [HttpPost]
        public async Task<IActionResult> GetAllUsers(int pageNumber,int pageSize, [FromBody] GetUsersRequestDto usersRequest) {
            var result = await _userServices.GetAllUsersAsync(usersRequest, pageNumber, pageSize);
            return Success(result);
        }
    }
}
