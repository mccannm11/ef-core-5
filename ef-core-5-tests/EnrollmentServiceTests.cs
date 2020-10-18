using System;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using ef_core_5.Data;
using ef_core_5.Services.Enrollment;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ef_core_5_tests
{
    public class AutoMockAttribute : AutoDataAttribute
    {
        private static readonly Func<IFixture> FixtureFactory = () =>
        {
            var fixture = new Fixture()
                .Customize(new AutoMoqCustomization());
            
            fixture.Register(ContextFactory.GetSchoolContext);
            // fixture.Customize(new StudentCustomization());
            fixture.Customize<Student>(
                c => c.Without(s => s.PersonId));

            fixture.Customize<Person>(c => c.Without(p => p.PersonId));

            return fixture;
        };

        public AutoMockAttribute()
            : base(FixtureFactory)
        {
        }
    }


    public static class ContextFactory
    {
        public static SchoolContext GetSchoolContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<SchoolContext>()
                .EnableSensitiveDataLogging()
                .UseSqlite(connection)
                .Options;

            var context = new SchoolContext(options);
            context.Database.EnsureCreated();

            return context;
        }
    }

    public class EnrollmentServiceTests
    {
        [Theory, AutoMock]
        public void TheAutoMoqAttributeShouldWork(
            EnrollmentService enrollmentService)
        {
            enrollmentService.Should().NotBe(null);
        }

        [Theory, AutoMock]
        public async Task EnrollStudentInCourseSection_Should_ReturnStudentNotFound(
            EnrollmentService enrollmentService)
        {
            var result = await enrollmentService.EnrollStudentInCourseSection(1, 1);
            result.Status.Should().Be(EnrollmentResultStatus.StudentNotFound);
        }

        [Theory, AutoMock]
        public async Task EnrollStudentInCourseSection_Should_ReturnCourseNotFound(
            EnrollmentService enrollmentService,
            Student student,
            SchoolContext schoolContext)
        {
            student.Person = null;
            schoolContext.Add(student);
            await schoolContext.SaveChangesAsync();

            var result = await enrollmentService.EnrollStudentInCourseSection(student.StudentId, 1);
            result.Status.Should().Be(EnrollmentResultStatus.CourseSectionNotFound);
        }
    }
}