using System.ComponentModel.DataAnnotations;

namespace ProyectoDAWBackend.Models;

public class Pregunta {
  [Key]
  public int Id { get; set; }
  
  [Required]
  public string TituloPregunta { get; set; }

  [Required] 
  public string TextoPregunta { get; set; }

  [Required] 
  public string UsuarioPregunta { get; set; }
  
  public ICollection<Respuesta>? Respuestas { get; set; }
}