using UniversityApiBackend.Enums;
using System.ComponentModel.DataAnnotations;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Models.Dtos
{
    public class AddOrEditCourseDto : BaseEntity
    {
        [Required, MaxLength(50)]
        public string? Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string? TargetAudiences { get; set; }
        public string? Objetives { get; set; }
        public string? Requirements { get; set; }
        public ICollection<int>? CategoryId { get; set; }
        public Level Level { get; set; } = Level.Basic;
    }
}
