using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.Services;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.Dtos;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UniversityDBContex _context;
        private readonly IStudentService _studentService;

        public StudentsController(UniversityDBContex context, IStudentService studentService, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudents()
        {
            List<Student> student = await _context.Students.Include(s => s.Courses)
                .Where(s => !s.IsDeleted).ToListAsync();
            if (student.Count <= 0)
                return NotFound("Aun no hay estudiantes registrados");

            return Ok(_mapper.Map<IEnumerable<StudentDto>>(student));
        }

        //6. Obtain the Courses of a Student
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetCourseOfStudent(int id)
        {
            Student? student = await _context.Students.Include(s => s.Courses!)
                .ThenInclude(c => c.Course).FirstOrDefaultAsync(s => s.Id == id);

            if (student is null)
                return NotFound();

            return Ok(_mapper.Map<StudentDto>(student));
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostStudent(AddOrEditStudentDto studentDto)
        {
            Student student = _mapper.Map<Student>(studentDto);
            _context.Students.Add(student);

            if (studentDto.CoursesId!.Count >= 1)
            {
                foreach (int courseId in studentDto.CoursesId)
                {
                    Course? course = await _context.Courses.Where(c => !c.IsDeleted).FirstOrDefaultAsync(c => c.Id == courseId);
                    if (course != null)
                    {
                        StudentCourse studentCourse = new()
                        {
                            Course = course,
                            Student = student
                        };

                        _context.StudentCourses.Add(studentCourse);
                    }
                }
            }

            await _context.SaveChangesAsync();

            return Ok(true);
        }

        //2. Get all students who do not have associated courses
        [Route("StudentWithoutCourses")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudent()
        {
            IEnumerable<Student> students = await _context.Students.Include(s => s.Courses)
                .Where(s => s.Courses!.Count <= 0 && !s.IsDeleted).ToListAsync();

            return Ok(_mapper.Map<IEnumerable<StudentDto>>(students));
        }

        //5. Get students from a specific Course
        [Route("StudentsFromASpecificCourse")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCourseDetailsDto>>> GetStudentFromCourse(int courseId)
        {
            List<StudentCourse> studentCourse = await _context.StudentCourses.Include(sc => sc.Course)
                .Include(sc => sc.Student).Where(sc => sc.CourseId == courseId).ToListAsync();
            if (studentCourse.Count <= 0)
                return NotFound("El curso no existe");

            return Ok(_mapper.Map<IEnumerable<StudentCourseDetailsDto>>(studentCourse));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, AddOrEditStudentDto studentDto)
        {
            if (id != studentDto.Id || string.IsNullOrEmpty(studentDto.UpdatedBy))
                return BadRequest("Datos invalidos");

            Student? student = await _context.Students.FindAsync(id);
            if (student is null)
                return BadRequest("el estudiante no existe");

            studentDto.Dob = studentDto.Dob;
            student.UpdatedAt = DateTime.Now;
            student.UpdatedBy = studentDto.UpdatedBy;
            student.LastName = studentDto.LastName ?? student.LastName;
            student.FirstName = studentDto.FirstName ?? student.FirstName;

            if (studentDto.CoursesId != null)
            {
                foreach (int courseId in studentDto.CoursesId)
                {
                    Course? course = await _context.Courses.FindAsync(courseId);
                    if (course != null)
                    {
                        StudentCourse studentCourse = new()
                        {
                            Course = course,
                            Student = student
                        };
                        _context.StudentCourses.Add(studentCourse);
                    }
                }
            }

            _context.Students.Update(student);

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
        public async Task<ActionResult<bool>> DeleteStudent(int id, string deleteBy)
        {
            Student? student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            student.IsDeleted = true;
            student.DeletedBy = deleteBy;
            student.DeletedAt = DateTime.UtcNow;

            _context.Students.Update(student);
            return Ok(await _context.SaveChangesAsync() > 0);
        }
    }
}
