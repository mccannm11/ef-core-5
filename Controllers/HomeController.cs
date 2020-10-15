using System.Linq;
using System.Threading.Tasks;
using ef_core_5.Data;
using ef_core_5.Services.Enrollment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ef_core_5.Controllers
{
    [Route("/")]
    public class HomeController : ControllerBase
    {
        private readonly SchoolContext _context;
        private readonly IEnrollmentService _enrollmentService;

        public HomeController(SchoolContext context, IEnrollmentService enrollmentService)
        {
            _context = context;
            _enrollmentService = enrollmentService;
        }

        public async Task<IActionResult> Index()
        {
            var student = _context.Students
                .Include(s => s.CourseSections)
                .Include(s => s.Person)
                .First();

            var courseSections = _context.CourseSections.ToList();

            var firstEnrollmentResult = await
                _enrollmentService.EnrollStudentInCourseSection(student.StudentId,
                    courseSections.First().CourseSectionId);

            var secondEnrollmentResult =
                await _enrollmentService.EnrollStudentInCourseSection(student.StudentId,
                    courseSections.First().CourseSectionId);


            return Ok(new {firstEnrollmentResult, secondEnrollmentResult});
        }
    }
}