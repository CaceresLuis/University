namespace UniversityApiBackend.Models.Dtos
{
    public class CourseCategoryDetailsDto
    {
        public int CourseId { get; set; }
        public string? CourseName { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set;}
    }
}
