using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain.Common;

namespace Domain.Entities {
    public class Course {
        public int Id { get; private set; }

        [Required, MaxLength(200)]
        public string Title { get; private set; }

        [MaxLength(1000)]
        public string Description { get; private set; }

        [Required]
        public DateTime StartDate { get; private set; }

        [Required]
        public DateTime EndDate { get; private set; }

        [Required]
        public int Capacity { get; private set; }

        [Required]
        public decimal Fee { get; private set; }

        [Required, MaxLength(100)]
        public string TeacherName { get; private set; }

        public ICollection<Enrollment> Enrollments { get; private set; } = new List<Enrollment>();
        private Course() { }
        public Course(string title, string description, DateTime startDate, DateTime endDate, int capacity, decimal fee, string teacherName) {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Title required");
            if (startDate >= endDate)
                throw new DomainException("Invalid dates");
            if (capacity <= 0)
                throw new DomainException("Capacity must be > 0");
            if (fee < 0)
                throw new DomainException("Fee cannot be negative");
            if (string.IsNullOrWhiteSpace(teacherName))
                throw new DomainException("TeacherName required");

            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Capacity = capacity;
            Fee = fee;
            TeacherName = teacherName;
        }

        public bool HasAvailableSeats() => Enrollments.Count < Capacity;

        public void EnrollStudent(Student student) {
            if (!HasAvailableSeats())
                throw new DomainException("Course full");
            if (student.IsEnrolledInCourse(Id))
                throw new DomainException("Student already enrolled");
            Enrollments.Add(new Enrollment(student.Id, Id));
        }
        public void Update(string title, string description, DateTime startDate, DateTime endDate, int capacity, decimal fee, string teacherName) {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Title required");
            if (startDate >= endDate)
                throw new DomainException("Invalid dates");
            if (capacity < Enrollments.Count)
                throw new DomainException($"Cannot reduce capacity below current enrollment count ({Enrollments.Count}).");
            if (fee < 0)
                throw new DomainException("Fee cannot be negative");
            if (string.IsNullOrWhiteSpace(teacherName))
                throw new DomainException("TeacherName required");

            Title = title;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Capacity = capacity;
            Fee = fee;
            TeacherName = teacherName;
        }
    }
}
