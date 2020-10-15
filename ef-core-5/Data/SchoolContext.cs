using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace ef_core_5.Data
{
    public class SchoolContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseSection> CourseSections { get; set; }
        public DbSet<CourseSectionEnrollment> CourseSectionEnrollments { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<StudentAssignment> StudentAssignments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=school.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseSectionEnrollment>().HasKey(e => new {e.CourseSectionId, e.StudentId});
        }

        public void SetupDevelopmentDatabase()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            var teacher = new Teacher()
            {
                Person = new Person()
                {
                    FirstName = "Sally",
                    LastName = "Simpson"
                }
            };

            var student = new Student()
            {
                Person = new Person()
                {
                    FirstName = "Timmy",
                    LastName = "Timmons"
                }
            };

            var science = new Course()
            {
                Name = "Science"
            };
            var history = new Course()
            {
                Name = "History"
            };

            Add(teacher);
            Add(student);
            AddRange(science, history);

            SaveChanges();

            var historySection = new CourseSection()
            {
                CourseId = history.CourseId,
                TeacherId = teacher.TeacherId
            };

            Add(historySection);

            student.CourseSections = new Collection<CourseSection>()
            {
                historySection
            };

            SaveChanges();
        }
    }

    public class StudentAssignment
    {
        public int StudentAssignmentId { get; set; }
    }

    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }

    public class Student
    {
        public int StudentId { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public ICollection<CourseSection> CourseSections { get; set; }
    }

    public class Teacher
    {
        public int PersonId { get; set; }
        public int TeacherId { get; set; }
        public Person Person { get; set; }
        public ICollection<CourseSection> CourseSections { get; set; }
    }

    public class Assignment
    {
        public int AssignmentId { get; set; }
        public int CourseSectionId { get; set; }
    }

    public class CourseSectionEnrollment
    {
        public int CourseSectionId { get; set; }
        public int StudentId { get; set; }
    }

    public class CourseSection
    {
        public int CourseSectionId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<Student> Students { get; set; }
    }

    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
    }
}