using System.ComponentModel.DataAnnotations;

namespace ProyectoDAWBackend.Models; 

public class Respuesta {
  
  [Key] public int Id { get; set; }
  
  [Required] public string TextoRespuesta { get; set; }
  
  [Required] public string UsuarioRespuesta { get; set; }
  
  [Required] public int PreguntaId { get; set; }
}