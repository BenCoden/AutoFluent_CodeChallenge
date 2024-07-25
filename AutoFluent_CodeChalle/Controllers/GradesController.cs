using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoFluent_CodeChalle.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AutoFluent_CodeChalle.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GradesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Grades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grade>>> GetGrades()
        {
            return await _context.Grades.ToListAsync();
        }

        // GET: api/Grades/ClassId=5
        [HttpGet("ClassId={id}")]
        public async Task<ActionResult<IEnumerable<GradeJoinTable>>> GetGradeByClassId(int id)
        {
            var grade = await (from g in _context.Grades
                               join c in _context.Courses on g.CourseId equals c.CourseId
                               join st in _context.Students on g.StudentId equals st.StudentId
                               join sc in _context.Schools on c.SchoolId equals sc.SchoolId
                               join t in _context.Teachers on c.TeacherId equals t.TeacherId
                               where g.CourseId == id
                               select new GradeJoinTable()
                               { Student = st.Name, Class = c.Name, School = sc.Name, Teacher = t.Name, Grade = g.Value })
                                .ToListAsync();
            

            if (grade == null)
            {
                return NotFound();
            }

            return grade;
        }

        // GET: api/Grades/StudentId=5
        [HttpGet("StudentId={id}")]
        public async Task<ActionResult<IEnumerable<GradeJoinTable>>> GetGradeByStudentId(int id)
        {
             
            var grade = await (from g in _context.Grades
                               join c in _context.Courses on g.CourseId equals c.CourseId
                               join st in _context.Students on g.StudentId equals st.StudentId
                               join sc in _context.Schools on c.SchoolId equals sc.SchoolId
                               join t in _context.Teachers on c.TeacherId equals t.TeacherId
                               where g.StudentId == id
                               select new GradeJoinTable() 
                               { Student = st.Name, Class = c.Name, School = sc.Name, Teacher = t.Name, Grade = g.Value})
                               .ToListAsync();

            if (grade == null)
            {
                return NotFound();
            }

            return grade;
        }


        // PUT: api/Grades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrade(int id, Grade grade)
        {
            if (id != grade.GradeId)
            {
                return BadRequest();
            }

            _context.Entry(grade).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Grades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostGrade(Grade grade)
        {
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            return Ok($"{grade.GradeId} was updated");
        }

        // DELETE: api/Grades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();

            return Ok($"{id} was delted");
        }

        private bool GradeExists(int id)
        {
            return _context.Grades.Any(e => e.GradeId == id);
        }


    }
   


}
