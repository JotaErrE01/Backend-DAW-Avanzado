using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoDAWBackend.Data;
using ProyectoDAWBackend.Models;

namespace ProyectoDAWBackend.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class ProductoController : ControllerBase {
    private readonly ApplicationDbContext _context;

    public ProductoController(ApplicationDbContext context) {
      _context = context;
    }

    // GET: api/Producto
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos() {
      if (_context.Productos == null) {
        return NotFound();
      }

      return await _context.Productos.ToListAsync();
    }

    // GET: api/Producto/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> GetProducto(int id) {
      if (_context.Productos == null) {
        return NotFound();
      }

      var producto = await _context.Productos.FindAsync(id);

      if (producto == null) {
        return NotFound();
      }

      return producto;
    }
    
    // GET: api/Producto/bajos
    [HttpGet("producto/{tipo}")]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductosByTipo(string tipo) {
      if (_context.Productos == null) {
        return NotFound();
      }

      var producto = await _context.Productos.Where(p => p.Tipo.Nombre.Equals(tipo)).Include( p => p.Tipo).ToListAsync();

      if (producto == null) {
        return NotFound();
      }

      return producto;
    }

    // PUT: api/Producto/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> PutProducto(int id, Producto producto) {
      if (id != producto.Id) {
        return BadRequest();
      }

      _context.Entry(producto).State = EntityState.Modified;

      try {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException) {
        if (!ProductoExists(id)) {
          return NotFound();
        }
        else {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Producto
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<Producto>> PostProducto(Producto producto) {
      if (_context.Productos == null) {
        return Problem("Entity set 'ApplicationDbContext.Productos'  is null.");
      }

      _context.Productos.Add(producto);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
    }

    // DELETE: api/Producto/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteProducto(int id) {
      if (_context.Productos == null) {
        return NotFound();
      }

      var producto = await _context.Productos.FindAsync(id);
      if (producto == null) {
        return NotFound();
      }

      _context.Productos.Remove(producto);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ProductoExists(int id) {
      return (_context.Productos?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}