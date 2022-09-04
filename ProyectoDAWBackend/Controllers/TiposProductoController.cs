using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDAWBackend.Data;
using ProyectoDAWBackend.Models;

namespace ProyectoDAWBackend.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class TiposProductoController : ControllerBase {
    private readonly ApplicationDbContext _context;

    public TiposProductoController(ApplicationDbContext context) {
      _context = context;
    }

    // GET: api/TiposProducto
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoProducto>>> GetTiposProducto() {
      if (_context.TiposProducto == null) {
        return NotFound();
      }

      return await _context.TiposProducto.ToListAsync();
    }

    // GET: api/TiposProducto/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TipoProducto>> GetTipoProducto(int id) {
      if (_context.TiposProducto == null) {
        return NotFound();
      }

      var tipoProducto = await _context.TiposProducto.FindAsync(id);

      if (tipoProducto == null) {
        return NotFound();
      }

      return tipoProducto;
    }

    // PUT: api/TiposProducto/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTipoProducto(int id, TipoProducto tipoProducto) {
      if (id != tipoProducto.Id) {
        return BadRequest();
      }

      _context.Entry(tipoProducto).State = EntityState.Modified;

      try {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException) {
        if (!TipoProductoExists(id)) {
          return NotFound();
        }
        else {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/TiposProducto
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<TipoProducto>> PostTipoProducto(TipoProducto tipoProducto) {
      if (_context.TiposProducto == null) {
        return Problem("Entity set 'ApplicationDbContext.TiposProducto'  is null.");
      }

      _context.TiposProducto.Add(tipoProducto);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetTipoProducto", new { id = tipoProducto.Id }, tipoProducto);
    }

    // DELETE: api/TiposProducto/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTipoProducto(int id) {
      if (_context.TiposProducto == null) {
        return NotFound();
      }

      var tipoProducto = await _context.TiposProducto.FindAsync(id);
      if (tipoProducto == null) {
        return NotFound();
      }

      _context.TiposProducto.Remove(tipoProducto);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool TipoProductoExists(int id) {
      return (_context.TiposProducto?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}