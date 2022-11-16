using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class StudentService : IStudentService
    {
        public IEnumerable<Student> GetCoursesOfAStudent(int studentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetStudentsFromASpecificCourse(int courseId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetStudentsWithCourses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetStudentsWithoutCourses()
        {
            throw new NotImplementedException();
        }
    }
}
