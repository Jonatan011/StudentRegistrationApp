using Microsoft.AspNetCore.Mvc;
using StudentManagement.Data.Entities;
using StudentManagement.Services;
using StudentManagement.Models;


namespace StudentManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateStudentDto studentDto)
        {
            try
            {
                var student = await _studentService.RegisterStudentAsync(studentDto);
                if (student == null) return NotFound();
                return Ok(student);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("register-courses")]
        public async Task<IActionResult> RegisterCourses([FromBody] RegisterCoursesDto registerCoursesDto)
        {
            try
            {
                await _studentService.RegisterCoursesAsync(registerCoursesDto.StudentId, registerCoursesDto.CourseIds);
                return Ok(new { Message = "Cursos registrados exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateStudentDto studentDto)
        {
            try
            {
                await _studentService.UpdateStudentAsync(id, studentDto);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteStudentAsync(id);
            return NoContent();
        }
    }
}
