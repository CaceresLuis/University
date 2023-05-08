namespace UniversityApiBackend.Models.Dtos
{
    public class StudentCourseDetailsDto
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
    }
}
