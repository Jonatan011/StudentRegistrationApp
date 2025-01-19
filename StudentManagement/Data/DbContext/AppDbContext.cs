using Microsoft.EntityFrameworkCore;
using StudentManagement.Data.Entities;

namespace StudentManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourseRegistration> StudentCourseRegistrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la tabla de relación muchos a muchos
            modelBuilder.Entity<StudentCourseRegistration>()
                .HasKey(scr => new { scr.StudentId, scr.CourseId });

            modelBuilder.Entity<StudentCourseRegistration>()
                .HasOne(scr => scr.Student)
                .WithMany(s => s.StudentCourseRegistrations)
                .HasForeignKey(scr => scr.StudentId);

            modelBuilder.Entity<StudentCourseRegistration>()
                .HasOne(scr => scr.Course)
                .WithMany(c => c.StudentCourseRegistrations)
                .HasForeignKey(scr => scr.CourseId);
        }
    }
}
