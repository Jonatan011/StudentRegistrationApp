using StudentManagement.Models;

namespace StudentManagement.Models
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Credits { get; set; }
        public string ProfessorName { get; set; } = string.Empty;
    }
}
