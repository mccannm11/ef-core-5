using System.Linq;
using ef_core_5.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ef_core_5.Controllers
{
    [Route("/")]
    public class HomeController : ControllerBase
    {
        private readonly SchoolContext _context;

        public HomeController(SchoolContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var student = _context.Students
                .Include(s => s.CourseSections)
                .Include(s => s.Person)
                .First();

            var section = student.CourseSections.First();

            return Ok(new {section.TeacherId, section.CourseId});
        }
    }
}