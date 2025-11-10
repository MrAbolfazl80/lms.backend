using Application.Services;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Courses;
using SimpleLms.Controllers.Base;

namespace WebApi.Controllers {
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CourseController : LmsControllerBase {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService) {
            _courseService = courseService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10) {
            var result = await _courseService.GetPagedAsync(pageNumber, pageSize);
            return Success(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAvailableCourses([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, bool excludeRegistered = true) {
            int? userId = HttpContext.User.Identity.IsAuthenticated ? CurrentUserId : null;
            var result = await _courseService.GetAvailableCoursesAsync(pageNumber, pageSize, userId);
            return Success(result);
        }
        [HttpGet()]
        [Authorize]
        public async Task<IActionResult> GetEnrolledCourses(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10) {
            if (!HttpContext.User.Identity.IsAuthenticated)
                return Unauthorized();

            int userId = CurrentUserId;
            var result = await _courseService.GetEnrolledCoursesAsync(userId, pageNumber, pageSize);

            return Success(result);
        }
        [HttpGet("{id}")]
        //[Authorize(Roles = "Admin,Student")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id) {
            var result = await _courseService.GetByIdAsync(id);
            return Success(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCourseRequest request) {
            var result = await _courseService.CreateAsync(request);
            return Success(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCourseRequest request) {
            var result = await _courseService.UpdateAsync(id, request);
            return Success(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id) {
            var result = await _courseService.DeleteAsync(id);
            return Success(result);
        }
    }
}
