
namespace StudentManagement.Models
{
    public class RegisterCoursesDto
    {
        public int StudentId { get; set; }
        public List<int> CourseIds { get; set; }
    }

}