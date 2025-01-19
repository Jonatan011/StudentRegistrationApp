using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Data.Entities
{
    [Table("Courses")]
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Credits { get; set; } = 3;  // Default value

        [Required]
        public int ProfessorId { get; set; }

        [ForeignKey("ProfessorId")]
        public Professor Professor { get; set; }


        public ICollection<StudentCourseRegistration> StudentCourseRegistrations { get; set; } = new List<StudentCourseRegistration>();
    }
}
