using System.Threading.Tasks;

namespace ef_core_5.Services.Enrollment
{
    public interface IEnrollmentService
    {
        Task<EnrollmentResult> EnrollStudentInCourseSection(int studentId, int courseSectionId);
    }
}