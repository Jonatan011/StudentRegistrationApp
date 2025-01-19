namespace StudentManagement.Models
{
    public class ProfessorDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IEnumerable<CourseDto> Courses { get; set; } = new List<CourseDto>();
    }
}
