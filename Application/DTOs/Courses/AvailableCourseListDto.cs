using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Courses
{
    public class AvailableCourseListDto {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TeacherName { get; set; }
        public int Capacity { get; set; }
        public int EnrolledCount { get; set; }
        public int RemainingSeats => Capacity - EnrolledCount;
        public bool IsOpenForEnrollment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsEnrolledByCurrentUser { get; set; }
    }
}
