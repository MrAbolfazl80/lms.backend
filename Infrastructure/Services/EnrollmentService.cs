using Application.Repositories;
using Domain.Entities;
using Domain.Common;
using System;
using System.Threading.Tasks;
using Application.Services;

namespace Infrastructure.Services {
    public class EnrollmentService : IEnrollmentService {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollmentService(
            ICourseRepository courseRepository,
            IStudentRepository studentRepository,
            IEnrollmentRepository enrollmentRepository) {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<bool> EnrollAsync(int userId, int courseId) {
            var student = await _studentRepository.GetStudentByUserIdAsync(userId)
                ?? throw new DomainException("کاربر یافت نشد");

            var course = await _courseRepository.GetByIdWithEnrollmentsAsync(courseId)
                ?? throw new DomainException("دوره مورد نظر یافت نشد");

            if (DateTime.Now < course.StartDate || DateTime.Now > course.EndDate)
                throw new DomainException("در بازه زمانی ثبت نام قرار نداریم");

            if (!course.HasAvailableSeats())
                throw new DomainException("ظرفیت دوره تکمیل است");

            if (await _enrollmentRepository.ExistsAsync(student.Id, course.Id))
                throw new DomainException("دانشجو قبلا در این دوره ثبت نام کرده است");

            var enrollment = new Enrollment(student.Id, course.Id);
            await _enrollmentRepository.AddAsync(enrollment);
            return true;
        }

        public async Task<int[]> GetEnrolledCoursedIdsByUserIdAsync(int userId) {
            var student = await _studentRepository.GetStudentByUserIdAsync(userId)
                ?? throw new DomainException("کاربر یافت نشد");
            var enrolledCourseIds =await _enrollmentRepository.GetEnrollmentsIdsByUserId(userId);
            return enrolledCourseIds;
        }
    }
}
