using StudentManagement.Data.Entities;

namespace StudentManagement.Data.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task AddAsync(Student student);
        Task AddCourseStudentAsync(StudentCourseRegistration studentCourseRegistration);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
