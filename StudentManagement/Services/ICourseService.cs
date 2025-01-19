using StudentManagement.Models;

namespace StudentManagement.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto?> GetCourseByIdAsync(int id);
        Task AddCourseAsync(CreateOrUpdateCourseDto courseDto);
        Task UpdateCourseAsync(int id, CreateOrUpdateCourseDto courseDto);
        Task DeleteCourseAsync(int id);
    }
}
