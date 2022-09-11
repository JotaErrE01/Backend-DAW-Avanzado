using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProyectoDAWBackend.Models; 

public class Usuario {
  [Key] public int Id { get; set; }
  
  [Required] public string Nombre { get; set; }
  
  [Required] public string Apellidos { get; set; }
  
  [Required] [DefaultValue("client")] public string Role { get; set; }
  
  [Required] [EmailAddress]  public string Email { get; set; }
  
  [Required] public byte[] PasswordHash { get; set; }
  
  [Required] public byte[] PasswordSalt { get; set; }
}