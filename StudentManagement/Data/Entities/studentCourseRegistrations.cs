using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Data.Entities
{
    [Table("StudentCourseRegistrations")]
    public class StudentCourseRegistration
    {
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public  Course Course { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
    }
}
