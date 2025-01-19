using StudentManagement.Data.Entities;
using StudentManagement.Models;


namespace StudentManagement.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto?> GetStudentByIdAsync(int id);
        Task<StudentDto?> RegisterStudentAsync(CreateStudentDto studentDto);
        Task RegisterCoursesAsync(int studentId, List<int> courseIds);
        Task UpdateStudentAsync(int id, CreateStudentDto studentDto);
        Task DeleteStudentAsync(int id);
    }
}

