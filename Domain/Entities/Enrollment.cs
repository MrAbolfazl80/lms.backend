using Domain.Common;
using Domain.Entities;

public class Enrollment {
    public int Id { get; private set; }

    public int StudentId { get; private set; }
    public Student Student { get; private set; }

    public int CourseId { get; private set; }
    public Course Course { get; private set; }

    public DateTime EnrolledAt { get; private set; }

    private Enrollment() { }

    public Enrollment(int studentId, int courseId) {
        if (studentId <= 0)
            throw new DomainException("Invalid StudentId");
        if (courseId <= 0)
            throw new DomainException("Invalid CourseId");

        StudentId = studentId;
        CourseId = courseId;
        EnrolledAt = DateTime.Now;
    }
}
