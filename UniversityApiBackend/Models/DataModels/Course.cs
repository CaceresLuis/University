using UniversityApiBackend.Enums;
using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Course : BaseEntity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(280)]
        public string ShortDescription { get; set; } = string.Empty;
        public string LongDescription { get; set; } = string.Empty;
        public string TargetAudiences { get; set; } = string.Empty;
        public string Objetives { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
        public Level Level { get; set; } = Level.Basic;
        [Required]
        public Chapter Chapter { get; set; } = new Chapter();
        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();   
        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
