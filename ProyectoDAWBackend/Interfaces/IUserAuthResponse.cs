namespace ProyectoDAWBackend; 

public interface IUserAuthResponse {
  string nombre { get; set; }
  string apellido { get; set; }
  string email { get; set; }
  string token { get; set; }
  string rol { get; set; }
}