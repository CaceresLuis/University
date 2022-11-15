using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface ICategoryService
    {
        Category GetCategoryBySpecificCourse(int courseId); //4. Get category of a specific course
    }
}
