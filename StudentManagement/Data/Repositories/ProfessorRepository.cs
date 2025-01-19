using Microsoft.EntityFrameworkCore;
using StudentManagement.Data.Entities;

namespace StudentManagement.Data.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly AppDbContext _context;

        public ProfessorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Professor>> GetAllAsync()
        {
            return await _context.Professors.Include(p => p.Courses).ToListAsync();
        }

        public async Task<Professor?> GetByIdAsync(int id)
        {
            return await _context.Professors.Include(p => p.Courses).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Professor professor)
        {
            _context.Professors.Add(professor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Professor professor)
        {
            _context.Professors.Update(professor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var professor = await GetByIdAsync(id);
            if (professor != null)
            {
                _context.Professors.Remove(professor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
