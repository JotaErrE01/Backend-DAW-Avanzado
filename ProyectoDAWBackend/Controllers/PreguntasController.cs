using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDAWBackend.Data;
using ProyectoDAWBackend.Models;

namespace ProyectoDAWBackend.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class PreguntasController : ControllerBase {
    private readonly ApplicationDbContext _context;

    public PreguntasController(ApplicationDbContext context) {
      _context = context;
    }

    // GET: api/Preguntas
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pregunta>>> GetPreguntas() {
      if (_context.Preguntas == null) {
        return NotFound();
      }

      var preguntas = await _context.Preguntas.Include(p => p.Respuestas).ToListAsync();
      
      return preguntas;
    }

    // GET: api/Preguntas/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Pregunta>> GetPregunta(int id) {
      if (_context.Preguntas == null) {
        return NotFound();
      }

      var pregunta = await _context.Preguntas.FindAsync(id);

      if (pregunta == null) {
        return NotFound();
      }

      return pregunta;
    }

    // PUT: api/Preguntas/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPregunta(int id, Pregunta pregunta) {
      if (id != pregunta.Id) {
        return BadRequest();
      }

      _context.Entry(pregunta).State = EntityState.Modified;

      try {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException) {
        if (!PreguntaExists(id)) {
          return NotFound();
        }
        else {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Preguntas
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Pregunta>> PostPregunta(Pregunta pregunta) {
      if (_context.Preguntas == null) {
        return Problem("Entity set 'ApplicationDbContext.Preguntas'  is null.");
      }

      _context.Preguntas.Add(pregunta);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetPregunta", new { id = pregunta.Id }, pregunta);
    }

    // DELETE: api/Preguntas/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePregunta(int id) {
      if (_context.Preguntas == null) {
        return NotFound();
      }

      var pregunta = await _context.Preguntas.FindAsync(id);
      if (pregunta == null) {
        return NotFound();
      }

      _context.Preguntas.Remove(pregunta);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool PreguntaExists(int id) {
      return (_context.Preguntas?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}