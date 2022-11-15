using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface ICourseService
    {
        IEnumerable<Course> GetCoursesByCategory(int categoryId); //1. Get all the Courses of a specific category
        IEnumerable<Course> GetUncategorizedCourses(); //3. Get Uncategorized Courses
    }
}
