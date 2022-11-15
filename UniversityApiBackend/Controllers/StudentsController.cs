using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;
using UniversityApiBackend.Services;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly UniversityDBContex _context;
        private readonly IStudentService _studentService;

        public StudentsController(UniversityDBContex context, IStudentService studentService)
        {
            _context = context;
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        //2. Get all students who do not have associated courses
        [Route("StudentWithoutCourses")]
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudent()
        {
            IEnumerable<Student> students = _studentService.GetStudentsWithoutCourses();

            return Ok(students);
        }

        //5. Get students from a specific Course
        [Route("StudentWithoutCourses")]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Student>> GetStudentFromCourse(int id)
        {
            IEnumerable<Student> students = _studentService.GetStudentsFromASpecificCourse(id);

            return Ok(students);
        }

        //6. Obtain the Courses of a Student
        [Route("StudentWithoutCourses")]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Student>> GetCourseOfStudent(int id)
        {
            IEnumerable<Student> students = _studentService.GetCoursesOfAStudent(id);

            return Ok(students);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            Student student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
