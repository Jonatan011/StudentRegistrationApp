using StudentManagement.Models;

namespace StudentManagement.Models
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IEnumerable<string> CourseNames { get; set; } = new List<string>();
        public IEnumerable<int> CourseIds { get; set; } = new List<int>();

    }
}
