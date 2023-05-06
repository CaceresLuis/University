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
            CreateMap<Course, AddOrEditCourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CourseCategory, CourseCategoryDto>().ReverseMap();
            CreateMap<CourseCategory, CourseCategoryDetailsDto>().ReverseMap();
        }
    }
}
