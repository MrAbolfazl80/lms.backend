using Application.Common;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace SimpleLms.Controllers.Base {
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class LmsControllerBase: ControllerBase {

        protected int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        protected string CurrentUsername => User.FindFirstValue(ClaimTypes.Name);
        protected string CurrentUserRole => User.FindFirstValue(ClaimTypes.Role);

        protected IActionResult Success<T>(T data) => Ok(BaseResponse<T>.Ok(data));
        protected IActionResult Failure<T>(string error) => BadRequest(BaseResponse<T>.Fail(error));
    }
}
