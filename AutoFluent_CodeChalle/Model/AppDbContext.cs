using Microsoft.EntityFrameworkCore;

namespace AutoFluent_CodeChalle.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<School> Schools { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<School>().HasData(new School() { SchoolId = 1, Name ="Senior High"}, new School() { SchoolId = 2, Name = "Roberts Middle School" }, new School() { SchoolId = 3, Name = "Janes Elementary" },
                new School() { SchoolId = 4, Name = "West Academy" });
            modelBuilder.Entity<Teacher>().HasData(new Teacher() { TeacherId = 1, Name = "Alert" }, new Teacher() { TeacherId = 2, Name = "Beth" }, new Teacher() { TeacherId = 3, Name = "Charlie" },
                new Teacher() { TeacherId = 4, Name ="Daisy"});
            modelBuilder.Entity<Student>().HasData(new Student() { StudentId = 1, Name = "Earl" }, new Student() { StudentId = 2, Name = "Farah" }, new Student() { StudentId = 3, Name = "Gus" },
                new Student() { StudentId = 4, Name = "Herruest" }, new Student() { StudentId = 5, Name = "Julio" }, new Student() { StudentId = 6, Name = "Kate" },
                new Student() { StudentId = 7, Name = "Less" }, new Student() { StudentId = 8, Name = "Mary" });
            modelBuilder.Entity<Grade>().HasData(new Grade() {GradeId = 1, CourseId = 1, StudentId = 8, Value = 92},new Grade() { GradeId = 2, CourseId = 2, StudentId = 7, Value = 87 }, new Grade() { GradeId = 3, CourseId = 3, StudentId = 6, Value = 72 },
                new Grade() { GradeId = 4, CourseId = 4, StudentId = 5, Value = 94 }, new Grade() { GradeId = 5, CourseId = 5, StudentId = 4, Value = 94 }, new Grade() { GradeId = 6, CourseId = 6, StudentId = 3, Value = 97 },
                new Grade() { GradeId = 7, CourseId = 1, StudentId = 2, Value = 86 }, new Grade() { GradeId = 8, CourseId = 2, StudentId = 1, Value = 65 }, new Grade() { GradeId = 9, CourseId = 3, StudentId = 8, Value = 77 });
            modelBuilder.Entity<Course>().HasData(
                new Course() { CourseId = 1, SchoolId = 1, TeacherId = 4, Name = "CS" }, new Course { CourseId = 2, SchoolId = 3, TeacherId = 3, Name = "Math" }, new Course() { CourseId = 3, SchoolId = 3, TeacherId = 2, Name = "Physics" },
                new Course { CourseId = 4, SchoolId = 4, TeacherId = 1, Name = "English" }, new Course() { CourseId = 5, SchoolId = 1, TeacherId = 4, Name = "Art" }, new Course { CourseId = 6, SchoolId = 2, TeacherId = 3, Name = "History" });
        }
    }
    public class School
    {
        public int SchoolId { get; set; }
        public string Name { get; set; }
    }
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
    }
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
    }
    public class Course 
    {
        public int CourseId { get; set; }
        public int SchoolId { get; set; }
        public int TeacherId { get; set; }
        public string Name { get; set; }
    }
    public class Grade
    {
        public int GradeId { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get;set; }
        public int Value { get;set;}

    }
}
