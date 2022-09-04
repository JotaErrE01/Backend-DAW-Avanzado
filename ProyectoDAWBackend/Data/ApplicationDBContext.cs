using Microsoft.EntityFrameworkCore;
using ProyectoDAWBackend.Models;

namespace ProyectoDAWBackend.Data; 

public class ApplicationDbContext: DbContext {

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
  
  // TABLES
  public DbSet<Producto> Productos { get; set; }
  public DbSet<Pregunta> Preguntas { get; set; }
  public DbSet<Respuesta> Respuestas { get; set; }
  
  public DbSet<Usuario> Usuarios { get; set; }
  public DbSet<TipoProducto> TiposProducto { get; set; }
}