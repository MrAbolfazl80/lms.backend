using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleLms.Controllers.Base;
using System.Threading.Tasks;

namespace SimpleLms.Controllers {
    [Authorize(Roles = "Admin,Student")]
    public class EnrollmentController : LmsControllerBase {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService) {
            _enrollmentService = enrollmentService;
        }
        [HttpPost("{courseId:int}")]
        public async Task<IActionResult> Enroll(int courseId) {
            var result = await _enrollmentService.EnrollAsync(CurrentUserId, courseId);
            return result
                ? Success("Enrolled successfully.")
                : Failure<string>("Enrollment failed.");
        }
    }
}
