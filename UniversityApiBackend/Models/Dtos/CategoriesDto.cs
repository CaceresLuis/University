using System.ComponentModel.DataAnnotations;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Models.Dtos
{
    public class CategoryDto : BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        //public ICollection<CourseCategoryDto>? CourseCategoryDtos { get; set; }
    }
}
