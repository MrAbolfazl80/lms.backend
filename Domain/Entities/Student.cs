using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain.Common;

namespace Domain.Entities {
    public class Student {
        public int Id { get; private set; }

        [Required, MaxLength(100)]
        public string FullName { get; private set; }
        [Required]
        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<Enrollment> Enrollments { get; private set; } = new List<Enrollment>();
        private Student() { }
        public Student(string fullName, int userId) {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("FullName required");

            if (userId <= 0)
                throw new DomainException("Invalid UserId");

            FullName = fullName;
            UserId = userId;
        }

        public bool IsEnrolledInCourse(int courseId) => Enrollments.Any(e => e.CourseId == courseId);
    }
}
