using Microsoft.EntityFrameworkCore;
using StudentManagement.Data.Entities;

namespace StudentManagement.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.Include(s => s.StudentCourseRegistrations)
                                          .ThenInclude(scr => scr.Course)
                                          .ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.Include(s => s.StudentCourseRegistrations)
                                          .ThenInclude(scr => scr.Course)
                                          .FirstOrDefaultAsync(s => s.Id == id);
        }


        public async Task AddAsync(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task AddCourseStudentAsync(StudentCourseRegistration studentCourseRegistration)
        {
            _context.StudentCourseRegistrations.Add(studentCourseRegistration);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Students.AnyAsync(s => s.Id == id);
        }
    }
}
