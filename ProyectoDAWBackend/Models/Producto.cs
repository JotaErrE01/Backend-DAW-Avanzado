using System.ComponentModel.DataAnnotations;

namespace ProyectoDAWBackend.Models;

public class Producto {
  [Key] public int Id { get; set; }

  [Required] public string Marca { get; set; }

  [Required] public string Modelo { get; set; }

  [Required] public string Descripcion { get; set; }

  [Required] public string Imagen { get; set; }

  [Required] public double Precio { get; set; }

  [Required] public int TipoProductoId { get; set; }
  public TipoProducto? Tipo { get; set; }
}