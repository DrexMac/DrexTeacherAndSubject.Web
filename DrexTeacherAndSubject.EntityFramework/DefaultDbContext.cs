using DrexTeacherAndSubject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DrexTeacherAndSubject.EntityFramework
{

    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Teacher>? Teachers { get; set; }
        public DbSet<Subject>? Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define non-nullable Guid values for Teachers and Subjects
            Guid teacherId1 = Guid.NewGuid();
            Guid teacherId2 = Guid.NewGuid();
            Guid teacherId3 = Guid.NewGuid();

            Guid subjectId1 = Guid.NewGuid();
            Guid subjectId2 = Guid.NewGuid();
            Guid subjectId3 = Guid.NewGuid();

            // Create lists of Teachers
            List<Teacher> teachers = new List<Teacher>
            {
                new Teacher
                {
                    Id = teacherId1,
                    Name = "Fym Macaspac",
                    Department = "Computer Studies Department"
                },
                new Teacher
                {
                    Id = teacherId2,
                    Name = "Rolly Macaspac",
                    Department = "BSIS Department"
                },
                new Teacher
                {
                    Id = teacherId3,
                    Name = "Josemaria Del Rosario",
                    Department = "Astro Department"
                }
            };

            List<Subject> subjects = new List<Subject>
            {
                new Subject
                {
                    Id = subjectId1,
                    Title = "Algebra",
                    CreditHours = 3
                },
                new Subject
                {
                    Id = subjectId2,
                    Title = "Physics",
                    CreditHours = 4
                },
                new Subject
                {
                    Id = subjectId3,
                    Title = "English",
                    CreditHours = 3
                }
            };

            // Seed Teachers and Subjects
            modelBuilder.Entity<Teacher>().HasData(teachers);
            modelBuilder.Entity<Subject>().HasData(subjects);

            // Configure Many-to-Many Relationship between Teachers and Subjects
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Subjects)
                .WithMany(s => s.Teachers)
                .UsingEntity<Dictionary<string, object>>(
                    "TeacherSubject", // Join table name
                    j => j.HasOne<Subject>().WithMany().HasForeignKey("SubjectId"),
                    j => j.HasOne<Teacher>().WithMany().HasForeignKey("TeacherId")
                );

            // Seed Many-to-Many Relationships
            modelBuilder.Entity("TeacherSubject").HasData(
                new { TeacherId = teacherId1, SubjectId = subjectId1 }, // Fym Macaspac - Algebra
                new { TeacherId = teacherId2, SubjectId = subjectId2 }, // Rolly Macaspac - Physics
                new { TeacherId = teacherId3, SubjectId = subjectId3 }  // Josemaria Del Rosario - English
            );
        }
    }

}
