using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProyectoDAWBackend.Data;
using ProyectoDAWBackend.Models;

namespace ProyectoDAWBackend.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase {
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    public static Usuario usuario = new Usuario();

    public AuthController(ApplicationDbContext context, IConfiguration configuration) {
      _context = context;
      _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<ActionResult<IUserAuthResponse>> Register(DatosUsuarioRegistrar request) {
      if (_context.Usuarios == null) {
        return NotFound();
      }

      CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

      usuario.Nombre = request.Nombre;
      usuario.Apellidos = request.Apellidos;
      usuario.Email = request.Email;
      usuario.Role = "client";
      usuario.PasswordHash = passwordHash;
      usuario.PasswordSalt = passwordSalt;

      // Guardando el usuario en la base de datos
      _context.Usuarios.Add(usuario);
      await _context.SaveChangesAsync();
      
      // Generando el token
      string token = CreateToken(usuario);

      // Devolver el token y el usuario
      return Ok(new { token, nombre = usuario.Nombre, apellidos = usuario.Apellidos, email = usuario.Email, role = usuario.Role });
    }

    [HttpPost("login")]
    public async Task<ActionResult<IUserAuthResponse>> Login(DatosUsuarioLoggear request) {
      if (_context.Usuarios == null) {
        return NotFound();
      }
    
      var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == request.Email);
      if (user == null) {
        return NotFound();
      }
    
      if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt)) {
        return Unauthorized();
      }
      
      string token = CreateToken(user);
    
      return Ok(new { token, nombre = user.Nombre, apellidos = user.Apellidos, email = user.Email, role = user.Role });
    }

    private string CreateToken(Usuario usuario) {
      List<Claim> claims = new List<Claim> {
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new Claim(ClaimTypes.Role, usuario.Role)
      };
    
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
    
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
    
      var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.Now.AddDays(1),
        signingCredentials: creds
      );
    
      var jwt = new JwtSecurityTokenHandler().WriteToken(token);
    
      return jwt;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
      using (var hmac = new HMACSHA512()) {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
      }
    }

    private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt) {
      using (var hmac = new HMACSHA512(storedSalt)) {
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++) {
          if (computedHash[i] != storedHash[i]) {
            return false;
          }
        }
      }
    
      return true;
    }
  }
}