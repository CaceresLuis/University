﻿using UniversityApiBackend.Enums;
using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Course : BaseEntity
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        public string? ShortDescription { get; set; }
        public string LongDescription { get; set; } = string.Empty;
        public string TargetAudiences { get; set; } = string.Empty;
        public string Objetives { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
        public Level Level { get; set; } = Level.Basic;
        public ICollection<StudentCourse>? Students { get; set; }  
        public ICollection<CourseCategory>? CourseCategories { get; set; }
    }
}
