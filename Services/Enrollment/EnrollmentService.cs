using System.Linq;
using System.Threading.Tasks;
using ef_core_5.Data;
using Microsoft.EntityFrameworkCore;

namespace ef_core_5.Services.Enrollment
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly SchoolContext _context;

        public EnrollmentService(SchoolContext context)
        {
            _context = context;
        }

        public async Task<EnrollmentResult> EnrollStudentInCourseSection(int studentId, int courseSectionId)
        {
            var student = await _context.Students.FindAsync(studentId);
            var courseSection = await _context.CourseSections.FindAsync(courseSectionId);

            if (student == null)
            {
                return new EnrollmentResult(EnrollmentResultStatus.StudentNotFound);
            }

            if (courseSection == null)
            {
                return new EnrollmentResult(EnrollmentResultStatus.CourseSectionNotFound);
            }

            var existingEnrollment = await _context.CourseSectionEnrollments
                .Where(e => e.StudentId == studentId)
                .Where(e => e.CourseSectionId == courseSectionId)
                .FirstOrDefaultAsync();

            if (existingEnrollment != null)
            {
                return new EnrollmentResult(EnrollmentResultStatus.StudentAlreadyEnrolled);
            }
            
            
            student.CourseSections.Add(courseSection);
            await _context.SaveChangesAsync();
            
            return new EnrollmentResult(EnrollmentResultStatus.SuccessfullyEnrolled);
        }
    }

    public class EnrollmentResult
    {
        public EnrollmentResult(EnrollmentResultStatus status)
        {
            Status = status;
        }

        public EnrollmentResultStatus Status { get; }

        public bool IsSuccess => Status == EnrollmentResultStatus.SuccessfullyEnrolled;
    }

    public enum EnrollmentResultStatus
    {
        CourseSectionNotFound,
        StudentNotFound,
        StudentAlreadyEnrolled,
        SuccessfullyEnrolled
    }
}