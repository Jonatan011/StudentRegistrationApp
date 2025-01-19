using StudentManagement.Data.Entities;
using StudentManagement.Data.Repositories;
using StudentManagement.Models;

namespace StudentManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public StudentService(IStudentRepository studentRepository, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return students.Select(s => new StudentDto
            {
                Id = s.Id,
                FullName = $"{s.FirstName} {s.LastName}",
                Email = s.Email,
                CourseNames = s.StudentCourseRegistrations.Select(scr => scr.Course.Name),
                CourseIds = s.StudentCourseRegistrations.Select(scr => scr.Course.Id)

            });
        }

        public async Task<StudentDto?> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null) return null;

            return new StudentDto
            {
                Id = student.Id,
                FullName = $"{student.FirstName} {student.LastName}",
                Email = student.Email,
                CourseNames = student.StudentCourseRegistrations.Select(scr => scr.Course.Name),
                CourseIds = student.StudentCourseRegistrations.Select(scr => scr.Course.Id)
            };
        }

        public async Task<StudentDto?> RegisterStudentAsync(CreateStudentDto studentDto)
        {
         
            var student = new Student
            {
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Email = studentDto.Email
            };

            await _studentRepository.AddAsync(student);

            return new StudentDto
            {
                Id = student.Id,
                FullName = $"{student.FirstName} {student.LastName}",
                Email = student.Email,
                CourseNames = [],
            };


        }
        public async Task RegisterCoursesAsync(int studentId, List<int> courseIds)
        {
            // Validar que los cursos sean válidos
            var existingCourses = await _courseRepository.GetByIdsAsync(courseIds);

            if (existingCourses.Count() != courseIds.Count)
            {
                throw new InvalidOperationException("Algunos de los cursos seleccionados no son válidos.");
            }

            // Obtener registros actuales del estudiante
            var existingRegistrations = await _studentRepository.GetByIdAsync(studentId);
            var existingCourseIds = existingRegistrations.StudentCourseRegistrations.Select(scr => scr.CourseId).ToList();

            // Validar que no se registre al mismo curso dos veces
            if (existingCourseIds.Any(courseId => courseIds.Contains(courseId)))
            {
                throw new InvalidOperationException("El estudiante ya está registrado en algunos de estos cursos.");
            }

            // Validar que no tengan más de un curso con el mismo profesor
            var allCourseIds = existingCourseIds.Concat(courseIds).ToList();

            var coursesToValidate = await _courseRepository.GetByIdsAsync(allCourseIds);
            if (coursesToValidate.GroupBy(c => c.ProfessorId).Any(g => g.Count() > 1))
            {
                Console.WriteLine("Cursos con el mismo profesor: ");
                foreach (var course in coursesToValidate.GroupBy(c => c.ProfessorId))
                {
                    Console.WriteLine($"ProfesorId: {course.Key}, Cantidad de materias: {course.Count()}");
                }

                throw new InvalidOperationException("El estudiante no puede tener clases con el mismo profesor en más de una materia.");
            }

            // Validar que el estudiante no seleccione más de 3 materias
            if (courseIds.Count > 3)
            {
                throw new InvalidOperationException("El estudiante no puede seleccionar más de 3 materias.");
            }
            // Crear registros
            var registrations = courseIds.Select(courseId => new StudentCourseRegistration
            {
                StudentId = studentId,
                CourseId = courseId
            }).ToList();

            foreach (var registration in registrations)
            {
                await _studentRepository.AddCourseStudentAsync(registration);
            }
        }




        public async Task UpdateStudentAsync(int id, CreateStudentDto studentDto)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null) throw new InvalidOperationException("El estudiante no existe.");

            student.FirstName = studentDto.FirstName;
            student.LastName = studentDto.LastName;
            student.Email = studentDto.Email;


            await _studentRepository.UpdateAsync(student);
        }

        public async Task DeleteStudentAsync(int id)
        {
            await _studentRepository.DeleteAsync(id);
        }
    }
}
