using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Category : BaseEntity
    {
        [Required]
        public string? Name { get; set; }

        public ICollection<CourseCategory>? CourseCategories { get; set; }
    }
}
