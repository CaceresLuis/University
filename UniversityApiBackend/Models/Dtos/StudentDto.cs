using System.ComponentModel.DataAnnotations;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Models.Dtos
{
    public class StudentDto : BaseEntity
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public DateTime Dob { get; set; }
        public ICollection<CourseDto>? Courses { get; set; }
    }
}
