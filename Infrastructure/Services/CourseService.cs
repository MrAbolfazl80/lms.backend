using Application.Common;
using Application.DTOs.Courses;
using Application.Repositories;
using Application.Services;
using Domain.Builders;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services {
    public class CourseService : ICourseService {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;

        public CourseService(ICourseRepository courseRepository, IStudentRepository studentRepository) {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }

        public async Task<PagedResult<CourseDto>> GetPagedAsync(int pageNumber, int pageSize) {
            var courses = await _courseRepository.GetAllWithEnrollmentsAsync();
            var query = courses.AsQueryable();

            var totalCount = query.Count();

            var items = query
                .OrderByDescending(c => c.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CourseDto {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Capacity = c.Capacity,
                    Fee = c.Fee,
                    TeacherName = c.TeacherName,
                    EnrolledCount = c.Enrollments.Count
                })
                .ToList();

            return new PagedResult<CourseDto>(items, totalCount, pageNumber, pageSize);
        }
        public async Task<PagedResult<AvailableCourseListDto>> GetAvailableCoursesAsync(int pageNumber, int pageSize, int? userId = null) {
            int? studentId = null;
            if (userId.HasValue) {
                studentId = await _studentRepository.GetStudentIdByUserIdAsync(userId.Value);
            }
            var now = DateTime.Now;
            var (courses, totalCount) = await _courseRepository.GetAvailablePagedAsync(pageNumber, pageSize, now);

            var result = courses.Select(c => new AvailableCourseListDto {
                Id = c.Id,
                Title = c.Title,
                TeacherName = c.TeacherName,
                Capacity = c.Capacity,
                EnrolledCount = c.Enrollments.Count,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                IsOpenForEnrollment = c.Enrollments.Count < c.Capacity,
                IsEnrolledByCurrentUser = studentId.HasValue
                    ? c.Enrollments.Any(x => x.StudentId == studentId.Value)
                    : false,
            }).ToList();

            return new PagedResult<AvailableCourseListDto>(result, totalCount, pageNumber, pageSize);
        }


        public async Task<CourseDto?> GetByIdAsync(int id) {
            var course = await _courseRepository.GetByIdWithEnrollmentsAsync(id);
            if (course == null)
                return null;

            return new CourseDto {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                Capacity = course.Capacity,
                Fee = course.Fee,
                TeacherName = course.TeacherName,
                EnrolledCount = course.Enrollments.Count
            };
        }

        public async Task<int> CreateAsync(CreateCourseRequest request) {
            var course = new CourseBuilder()
                .WithTitle(request.Title)
                .WithDescription(request.Description)
                .WithStartDate(request.StartDate)
                .WithEndDate(request.EndDate)
                .WithCapacity(request.Capacity)
                .WithFee(request.Fee)
                .WithTeacherName(request.TeacherName)
                .Build();

            await _courseRepository.AddAsync(course);
            return course.Id;
        }

        public async Task<bool> UpdateAsync(int id, UpdateCourseRequest request) {
            var course = await _courseRepository
                  .GetByIdWithEnrollmentsAsync(id);
            if (course == null)
                return false;

            var builder = new CourseBuilder()
                .WithTitle(request.Title)
                .WithDescription(request.Description)
                .WithStartDate(request.StartDate)
                .WithEndDate(request.EndDate)
                .WithCapacity(request.Capacity)
                .WithFee(request.Fee)
                .WithTeacherName(request.TeacherName);

            builder.ApplyTo(course);

            await _courseRepository.UpdateAsync(course);
            return true;
        }

        public async Task<bool> DeleteAsync(int id) {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
                return false;

            await _courseRepository.RemoveAsync(course);
            return true;
        }

        public async Task<PagedResult<UserEnrolledCourseListDto>> GetEnrolledCoursesAsync(int userId, int pageNumber, int pageSize) {
            int? studentId = await _studentRepository.GetStudentIdByUserIdAsync(userId);
            var now = DateTime.Now;
            var (courses, totalCount) = await _courseRepository.GetAvailablePagedAsync(pageNumber, pageSize, now, studentId);

            var result = courses
                .Select(course => new UserEnrolledCourseListDto {
                    Id = course.Id,
                    TeacherName = course.TeacherName,
                    Title = course.Title,
                    Fee = course.Fee,
                    EnrolledAt = course.Enrollments.FirstOrDefault(e => e.StudentId == studentId)?.EnrolledAt
                })
                .ToList();

            return new PagedResult<UserEnrolledCourseListDto>(result, totalCount, pageNumber, pageSize);
        }
    }
}
