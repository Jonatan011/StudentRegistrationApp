using StudentManagement.Data.Entities;
using StudentManagement.Data.Repositories;
using StudentManagement.Models;

namespace StudentManagement.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IProfessorRepository _professorRepository;

        public CourseService(ICourseRepository courseRepository, IProfessorRepository professorRepository)
        {
            _courseRepository = courseRepository;
            _professorRepository = professorRepository;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Name = c.Name,
                Credits = c.Credits,
                ProfessorName = c.Professor.Name
            });
        }

        public async Task<CourseDto?> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return null;

            return new CourseDto
            {
                Id = course.Id,
                Name = course.Name,
                Credits = course.Credits,
                ProfessorName = course.Professor.Name != null ? course.Professor.Name : "No Professor Assigned"
            };
        }

        public async Task AddCourseAsync(CreateOrUpdateCourseDto courseDto)
        {
            // Validar que el profesor exista
            var professor = await _professorRepository.GetByIdAsync(courseDto.ProfessorId);
            if (professor == null)
                throw new InvalidOperationException("El profesor especificado no existe.");

            // Crear curso y asignar ProfesorId
            var course = new Course
            {
                Name = courseDto.Name,
                Credits = courseDto.Credits,
                ProfessorId = courseDto.ProfessorId
            };

            // Al agregar el curso, se asigna el profesor existente
            await _courseRepository.AddAsync(course);
        }


        public async Task UpdateCourseAsync(int id, CreateOrUpdateCourseDto courseDto)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
                throw new InvalidOperationException("El curso no existe.");

            course.Name = courseDto.Name;
            course.Credits = courseDto.Credits;
            course.ProfessorId = courseDto.ProfessorId;

            await _courseRepository.UpdateAsync(course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            await _courseRepository.DeleteAsync(id);
        }
    }
}
