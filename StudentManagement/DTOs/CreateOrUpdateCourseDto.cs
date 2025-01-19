namespace StudentManagement.Models
{
    public class CreateOrUpdateCourseDto
    {
        public string Name { get; set; } = string.Empty;
        public int Credits { get; set; } = 3; // Default value
        public int ProfessorId { get; set; }
    }
}
