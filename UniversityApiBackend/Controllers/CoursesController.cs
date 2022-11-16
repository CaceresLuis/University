using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;
using UniversityApiBackend.Services;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly UniversityDBContex _context;
        private readonly ICourseService _courseService;

        public CoursesController(UniversityDBContex context, ICourseService courseService)
        {
            _context = context;
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        //1. Get all the Courses of a specific category
        [Route("ByCategory")]
        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetCoursesByCategory(int categoryId)
        {
            IEnumerable<Course> courses = _courseService.GetCoursesByCategory(categoryId);

            if (courses == null)
            {
                return NotFound();
            }

            return Ok(courses);
        }

        //3. Get Uncategorized Courses
        [Route("CoursesUncategorized")]
        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetCoursesUncategorized()
        {
            IEnumerable<Course> courses = _courseService.GetUncategorizedCourses();

            if (courses == null)
            {
                return NotFound();
            }

            return Ok(courses);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            Course? course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
