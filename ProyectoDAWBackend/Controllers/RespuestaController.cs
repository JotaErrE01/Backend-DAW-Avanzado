using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDAWBackend.Data;
using ProyectoDAWBackend.Models;

namespace ProyectoDAWBackend.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class RespuestaController : ControllerBase {
    private readonly ApplicationDbContext _context;

    public RespuestaController(ApplicationDbContext context) {
      _context = context;
    }

    // GET: api/Respuesta
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Respuesta>>> GetRespuestas() {
      if (_context.Respuestas == null) {
        return NotFound();
      }

      return await _context.Respuestas.ToListAsync();
    }

    // GET: api/Respuesta/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Respuesta>> GetRespuesta(int id) {
      if (_context.Respuestas == null) {
        return NotFound();
      }

      var respuesta = await _context.Respuestas.FindAsync(id);

      if (respuesta == null) {
        return NotFound();
      }

      return respuesta;
    }

    // PUT: api/Respuesta/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRespuesta(int id, Respuesta respuesta) {
      if (id != respuesta.Id) {
        return BadRequest();
      }

      _context.Entry(respuesta).State = EntityState.Modified;

      try {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException) {
        if (!RespuestaExists(id)) {
          return NotFound();
        }
        else {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Respuesta
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Respuesta>> PostRespuesta(Respuesta respuesta) {
      if (_context.Respuestas == null) {
        return Problem("Entity set 'ApplicationDbContext.Respuestas'  is null.");
      }

      _context.Respuestas.Add(respuesta);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetRespuesta", new { id = respuesta.Id }, respuesta);
    }

    // DELETE: api/Respuesta/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRespuesta(int id) {
      if (_context.Respuestas == null) {
        return NotFound();
      }

      var respuesta = await _context.Respuestas.FindAsync(id);
      if (respuesta == null) {
        return NotFound();
      }

      _context.Respuestas.Remove(respuesta);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool RespuestaExists(int id) {
      return (_context.Respuestas?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}