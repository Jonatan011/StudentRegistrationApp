using StudentManagement.Data.Entities;
using StudentManagement.Data.Repositories;
using StudentManagement.Models;

namespace StudentManagement.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly IProfessorRepository _professorRepository;

        public ProfessorService(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public async Task<IEnumerable<ProfessorDto>> GetAllProfessorsAsync()
        {
            var professors = await _professorRepository.GetAllAsync();
            return professors.Select(p => new ProfessorDto
            {
                Id = p.Id,
                Name = p.Name,
                Courses = p.Courses.Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Credits = c.Credits,
                    ProfessorName = p.Name
                }).ToList()
            });
        }

        public async Task<ProfessorDto?> GetProfessorByIdAsync(int id)
        {
            var professor = await _professorRepository.GetByIdAsync(id);
            if (professor == null) return null;

            return new ProfessorDto
            {
                Id = professor.Id,
                Name = professor.Name,
                Courses = professor.Courses.Select(c => new CourseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Credits = c.Credits,
                    ProfessorName = professor.Name
                }).ToList()
            };
        }

        public async Task AddProfessorAsync(CreateOrUpdateProfessorDto professorDto)
        {
            var professor = new Professor
            {
                Name = professorDto.Name,
            };

            await _professorRepository.AddAsync(professor);
        }

        public async Task UpdateProfessorAsync(int id, CreateOrUpdateProfessorDto professorDto)
        {
            var professor = await _professorRepository.GetByIdAsync(id);
            if (professor == null)
                throw new InvalidOperationException("El profesor no existe.");

            professor.Name = professorDto.Name;

            await _professorRepository.UpdateAsync(professor);
        }

        public async Task DeleteProfessorAsync(int id)
        {
            await _professorRepository.DeleteAsync(id);
        }
    }
}
