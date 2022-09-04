using System.ComponentModel.DataAnnotations;

namespace ProyectoDAWBackend.Models;

public class TipoProducto {
  [Key] public int Id { get; set; }
  
  [Required] public string Nombre { get; set; }
  
  public string? Descripcion { get; set; }
}