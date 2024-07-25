using AutoFluent_CodeChalle.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AutoFluent_CodeChalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AverageGradeController : Controller
    {
        private readonly AppDbContext _context;

        public AverageGradeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AverageGrade/TeacherId=5
        [HttpGet("TeacherId={id}")]
        public async Task<ActionResult<double>> GetByTeacher(int id)
        {
            var grade = await (_context.Grades.FromSql
                ($"SELECT  g.Value,g.CourseId,g.StudentId,g.GradeId from Teachers as t  Right Join Courses as c on t.TeacherId = c.TeacherId   Right Join Grades as g   on g.CourseId = c.CourseId   where t.TeacherId = {id}"))
                               .ToListAsync();
           
            if (grade == null)
            {
                return NotFound();
            }
            double avg = grade.Select((s) => s.Value).Sum() / (double)grade.Count;

            return avg;
        }
        // GET: api/AverageGrade/StudentId=5
        [HttpGet("StudentId={id}")]
        public async Task<ActionResult<double>> GetByStudent(int id)
        {
            var grade = await (_context.Grades.FromSql<Grade>($"select * from Grades where StudentId = {id}"))
                               .ToListAsync();

            if (grade == null)
            {
                return NotFound();
            }

            double avg = grade.Select((s) => s.Value).Sum() / (double)grade.Count;

            return avg;
        }
    }
}
