using AutoMapper;
using UniversityApiBackend.Models.Dtos;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Helpers
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Student, StudentDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Course, AddOrEditCourseDto>().ReverseMap();
            CreateMap<Student, AddOrEditStudentDto>().ReverseMap();
            CreateMap<StudentCourse, StudentCourseDto>().ReverseMap();
            CreateMap<CourseCategory, CourseCategoryDto>().ReverseMap();
            CreateMap<StudentCourse, StudentCourseDetailsDto>().ReverseMap();
            CreateMap<CourseCategory, CourseCategoryDetailsDto>().ReverseMap();
        }
    }
}