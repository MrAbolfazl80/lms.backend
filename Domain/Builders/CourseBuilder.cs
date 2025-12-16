using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Builders {
    public class CourseBuilder {
        private readonly InnerModel _model = new InnerModel();

        private class InnerModel {
            public string Title { get; set; } = "Default Title";
            public string Description { get; set; } = "Default Description";
            public DateTime StartDate { get; set; } = DateTime.UtcNow;
            public DateTime EndDate { get; set; } = DateTime.UtcNow.AddDays(1);
            public int Capacity { get; set; } = 10;
            public decimal Fee { get; set; } = 0;
            public string TeacherName { get; set; } = "Unknown Teacher";
        }

        public CourseBuilder WithTitle(string title) {
            _model.Title = title;
            return this;
        }

        public CourseBuilder WithDescription(string desc) {
            _model.Description = desc;
            return this;
        }

        public CourseBuilder WithStartDate(DateTime start) {
            _model.StartDate = start;
            return this;
        }

        public CourseBuilder WithEndDate(DateTime end) {
            _model.EndDate = end;
            return this;
        }

        public CourseBuilder WithCapacity(int capacity) {
            _model.Capacity = capacity;
            return this;
        }

        public CourseBuilder WithFee(decimal fee) {
            _model.Fee = fee;
            return this;
        }

        public CourseBuilder WithTeacherName(string teacher) {
            _model.TeacherName = teacher;
            return this;
        }

        public Course Build() => new Course(
                _model.Title,
                _model.Description,
                _model.StartDate,
                _model.EndDate,
                _model.Capacity,
                _model.Fee,
                _model.TeacherName
            );
        public CourseBuilder ApplyTo(Course course) {
            course.Update(
                _model.Title,
                _model.Description,
                _model.StartDate,
                _model.EndDate,
                _model.Capacity,
                _model.Fee,
                _model.TeacherName
            );
            return this;
        }
    }

}
