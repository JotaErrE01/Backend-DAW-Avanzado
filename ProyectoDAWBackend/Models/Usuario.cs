using System.ComponentModel.DataAnnotations;

namespace ProyectoDAWBackend.Models; 

public class Usuario {
  [Key] public int Id { get; set; }
  
  [Required] public string Nombre { get; set; }
  
  [Required] public string Apellidos { get; set; }
  
  [Required] public string Role { get; set; }
  
  [Required] public string Email { get; set; }
  
  [Required] public string Password { get; set; }
}