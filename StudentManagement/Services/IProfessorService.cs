using StudentManagement.Models;

namespace StudentManagement.Services
{
    public interface IProfessorService
    {
        Task<IEnumerable<ProfessorDto>> GetAllProfessorsAsync();
        Task<ProfessorDto?> GetProfessorByIdAsync(int id);
        Task AddProfessorAsync(CreateOrUpdateProfessorDto professorDto);
        Task UpdateProfessorAsync(int id, CreateOrUpdateProfessorDto professorDto);
        Task DeleteProfessorAsync(int id);
    }
}
