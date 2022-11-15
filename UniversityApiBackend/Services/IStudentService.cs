using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> GetStudentsWithCourses(); 
        IEnumerable<Student> GetStudentsWithoutCourses(); //2. Get all students who do not have associated courses
        IEnumerable<Student> GetStudentsFromASpecificCourse(int courseId); //5. Get students from a specific Course
        IEnumerable<Student> GetCoursesOfAStudent(int studentId); //6. Obtain the Courses of a Student
    }
}
