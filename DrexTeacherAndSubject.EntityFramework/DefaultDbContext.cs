using DrexTeacherAndSubject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DrexTeacherAndSubject.EntityFramework
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {
        }

        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            List<Teacher> teachers = new List<Teacher>()

            {
                new Teacher()
                {
                    TeacherId = 1,
                    Name = "Fym Macaspac",
                    Department = "Computer Studies Department"
                },
                new Teacher()
                {
                    TeacherId = 2,
                    Name = "Rolly Macaspac",
                    Department = "BSIS Department"
                }
            };

            
            List<Subject> subjects = new List<Subject>()

            {
                new Subject()
                {
                    SubjectId = 1,
                    Title = "Algebra",
                    CreditHours = 3
                },
                new Subject()
                {
                    SubjectId = 2,
                    Title = "Physics",
                    CreditHours = 4
                }
            };

            
            List<TeacherSubject> teacherSubjects = new List<TeacherSubject>()

            {
                new TeacherSubject()
                {
                    TeacherId = 1,
                    SubjectId = 1
                },  

                new TeacherSubject()
                {
                    TeacherId = 1,
                    SubjectId = 2
                },  

                new TeacherSubject()
                {
                    TeacherId = 2,
                    SubjectId = 1
                },
                new TeacherSubject()
                {
                    TeacherId = 2,
                    SubjectId = 2
                }
            };

            
            modelBuilder.Entity<TeacherSubject>()
                .HasKey(ts => new { ts.TeacherId, ts.SubjectId });  



            modelBuilder.Entity<TeacherSubject>()
                .HasOne(ts => ts.Teacher)
                .WithMany(t => t.TeacherSubjects)
                .HasForeignKey(ts => ts.TeacherId);



            modelBuilder.Entity<TeacherSubject>()
                .HasOne(ts => ts.Subject)
                .WithMany(s => s.TeacherSubjects)
                .HasForeignKey(ts => ts.SubjectId);


            
            modelBuilder.Entity<Teacher>().HasData(teachers);
            modelBuilder.Entity<Subject>().HasData(subjects);
            modelBuilder.Entity<TeacherSubject>().HasData(teacherSubjects);
        }
    }
}
