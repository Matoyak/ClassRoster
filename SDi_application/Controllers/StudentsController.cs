using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDi_application.Models;

// @TODO: Split database logic out of controller into depository
// @TODO: Split manipulation logic out of controller into service
// @TODO: Create Data Transfer Objects and View Models for what should be transferred around outside of the deepest backend layer
// (See SmartFridge Project for long-term intent)
namespace SDi_application.Controllers {
  [Produces("application/json")]
  [Route("api/Students")]
  public class StudentsController : Controller {
    private readonly ApplicationDbContext _context;

    public StudentsController( ApplicationDbContext context ) {
      _context = context;
      if ( _context.Students.Count() == 0 ) {
        _context.Students.Add(new Student {
          FirstName = "Jane",
          LastName = "Doe",
          DateOfBirth = DateTime.Parse("1990-12-05", new CultureInfo("en-US")),
          GPA = 3.4
        });
        _context.SaveChanges();
      }
    }

    // GET: api/Students
    [HttpGet]
    public IEnumerable<Student> GetStudents() {
      return _context.Students;
    }

    // GET: api/Students/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudent( [FromRoute] int id ) {
      if ( !ModelState.IsValid ) {
        return BadRequest(ModelState);
      }

      var student = await _context.Students.SingleOrDefaultAsync(m => m.Id == id);

      if ( student == null ) {
        return NotFound();
      }

      return Ok(student);
    }

    // PUT: api/Students/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudent( [FromRoute] int id, [FromBody] Student student ) {
      if ( !ModelState.IsValid ) {
        return BadRequest(ModelState);
      }

      if ( id != student.Id ) {
        return BadRequest();
      }

      _context.Entry(student).State = EntityState.Modified;

      try {
        await _context.SaveChangesAsync();
      } catch ( DbUpdateConcurrencyException ) {
        if ( !StudentExists(id) ) {
          return NotFound();
        } else {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Students
    [HttpPost]
    public async Task<IActionResult> PostStudent( [FromBody] Student student ) {
      if ( !ModelState.IsValid ) {
        return BadRequest(ModelState);
      }

      if ( student.GPA > 5 || student.GPA < 0 ) {
        return BadRequest("Invalid GPA range.");
      }

      _context.Students.Add(student);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetStudent", new { id = student.Id }, student);
    }

    // DELETE: api/Students/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent( [FromRoute] int id ) {
      if ( !ModelState.IsValid ) {
        return BadRequest(ModelState);
      }

      var student = await _context.Students.SingleOrDefaultAsync(m => m.Id == id);
      if ( student == null ) {
        return NotFound();
      }

      _context.Students.Remove(student);
      await _context.SaveChangesAsync();

      return Ok(student);
    }

    private bool StudentExists( int id ) {
      return _context.Students.Any(e => e.Id == id);
    }
  }
}
