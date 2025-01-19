using Microsoft.AspNetCore.Mvc;
using StudentManagement.Models;
using StudentManagement.Services;

namespace StudentManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorsController : ControllerBase
    {
        private readonly IProfessorService _professorService;

        public ProfessorsController(IProfessorService professorService)
        {
            _professorService = professorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var professors = await _professorService.GetAllProfessorsAsync();
            return Ok(professors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var professor = await _professorService.GetProfessorByIdAsync(id);
            if (professor == null) return NotFound();
            return Ok(professor);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateOrUpdateProfessorDto professorDto)
        {
            await _professorService.AddProfessorAsync(professorDto);
            return Created("", null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateOrUpdateProfessorDto professorDto)
        {
            await _professorService.UpdateProfessorAsync(id, professorDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _professorService.DeleteProfessorAsync(id);
            return NoContent();
        }
    }
}
