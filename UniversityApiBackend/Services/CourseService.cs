using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class CourseService : ICourseService
    {
        public IEnumerable<Course> GetCoursesByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Course> GetUncategorizedCourses()
        {
            throw new NotImplementedException();
        }
    }
}
