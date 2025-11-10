using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Courses
{
    public class UserEnrolledCourseListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TeacherName { get; set; }
        public decimal Fee { get; set; }
        public DateTime? EnrolledAt { get; set; }
        
    }
}
