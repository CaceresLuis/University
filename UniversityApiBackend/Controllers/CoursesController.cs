using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.Dtos;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UniversityDBContex _context;

        public CoursesController(UniversityDBContex context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            List<Course> courses = await _context.Courses.Include(c => c.CourseCategories).Where(c => !c.IsDeleted).ToListAsync();
            if(courses.Count <= 0)
                return NotFound();

            return Ok(_mapper.Map<List<CourseDto>>(courses));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            Course? course = await _context.Courses.Where(c => !c.IsDeleted)
                .Include(c => c.CourseCategories).FirstOrDefaultAsync(c => c.Id == id);
            if (course == null)
                return NotFound();

            return Ok(_mapper.Map<CourseDto>(course));
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostCourse(AddOrEditCourseDto courseDto)
        {
            Course course = _mapper.Map<Course>(courseDto);
            _context.Courses.Add(course);

            if (courseDto.CategoriesId!.Count >= 1)
            {
                foreach (int categoryId in courseDto.CategoriesId)
                {
                    Category? category = await _context.Categories.FindAsync(categoryId);
                    if (category != null)
                    {
                        CourseCategory courseCategory = new()
                        {
                            Course = course,
                            Category = category
                        };

                        _context.CourseCategories.Add(courseCategory);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return Ok(true);
        }

        //1. Get all the Courses of a specific category
        [Route("ByCategory")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseCategoryDetailsDto>>> GetCoursesByCategory(int categoryId)
        {
            List<CourseCategory> coursesCategory = await _context.CourseCategories.Include(cc => cc.Course)
                .Include(cc => cc.Category).Where(cc => cc.CategoryId == categoryId).ToListAsync();

            if (coursesCategory.Count <= 0)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<CourseCategoryDetailsDto>>(coursesCategory));
        }

        //3. Get Uncategorized Courses
        [Route("CoursesUncategorized")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesUncategorized()
        {
            List<Course> courses = await _context.Courses.Include(c => c.CourseCategories)
                .Where(c => c.CourseCategories!.Count <= 0 && !c.IsDeleted).ToListAsync();

            if (courses.Count <= 0)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<CourseDto>>(courses));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutCourse(int id, AddOrEditCourseDto courseDto)
        {
            if (id != courseDto.Id || string.IsNullOrEmpty(courseDto.UpdatedBy))
                return BadRequest("Datos invalidos");

            Course? course = await _context.Courses.FindAsync(id);
            if (course is null)
                return BadRequest("El curso no existee");

            course.Level = courseDto.Level;
            course.UpdatedAt = DateTime.Now;
            course.UpdatedBy = courseDto.UpdatedBy;
            course.Objetives = courseDto.Objetives ?? course.Objetives;
            course.Requirements = courseDto.Requirements ?? course.Requirements;
            course.TargetAudiences = courseDto.TargetAudiences ?? course.TargetAudiences;
            course.LongDescription = courseDto.LongDescription ?? course.LongDescription;
            course.ShortDescription = courseDto.ShortDescription ?? course.ShortDescription;

            if (courseDto.CategoriesId!.Count > 0)
            {
                foreach (int categoryId in courseDto.CategoriesId)
                {
                    Category? category = await _context.Categories.FindAsync(categoryId);
                    if (category != null)
                    {
                        CourseCategory courseCategory = new()
                        {
                            Course = course,
                            Category = category
                        };

                        _context.CourseCategories.Add(courseCategory);
                    }
                }
            }

            _context.Courses.Update(course);

            try
            {
                return Ok(await _context.SaveChangesAsync() >= 0);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCourse(int id, string deleteBy)
        {
            Course? course = await _context.Courses.FindAsync(id);
            if (course is null)
                return NotFound();

            course.IsDeleted = true;
            course.DeletedAt = DateTime.UtcNow;
            course.DeletedBy = deleteBy;

            _context.Courses.Update(course);

            return Ok(await _context.SaveChangesAsync() > 0);
        }
    }
}
