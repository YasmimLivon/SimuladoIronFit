using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimuladoIronFit.Data;
using SimuladoIronFit.Models;

namespace SimuladoIronFit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost ("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username.ToLower()== loginRequest.Username.ToLower());

            if (user == null || user.Password != loginRequest.Password)
            {
                return Unauthorized("Username ou senha incorretos.");
            }
           
            return Ok(new { Nome = user.Nome });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Usuario registerRequest)
        {
            
            if (await _context.Usuarios.AnyAsync(u => u.Username.ToLower() == registerRequest.Username.ToLower()))
            {
                return BadRequest("Este username já está em uso."); 
            }

            var novoUsuario = new Usuario
            {
                Nome = registerRequest.Nome,
                Username = registerRequest.Username,
                Password = registerRequest.Password
            };

            
            _context.Usuarios.Add(novoUsuario);

           
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Register), new { id = novoUsuario.Id }, new { novoUsuario.Nome, novoUsuario.Username });
        }
    }
}
