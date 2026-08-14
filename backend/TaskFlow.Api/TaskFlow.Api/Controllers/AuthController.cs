using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskFlow.Api.Data;
using TaskFlow.Api.Dtos;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TaskFlowDbContext _context;
        private readonly IConfiguration _config;
        private readonly PasswordHasher<Usuario> _passwordHasher = new();

        public AuthController(TaskFlowDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("registro")]
        public async Task<ActionResult<AuthResponseDto>> Registro(RegistroDto dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Ese email ya está registrado.");

            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email
            };
            usuario.PasswordHash = _passwordHasher.HashPassword(usuario, dto.Password);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var token = GenerarToken(usuario);
            return Ok(new AuthResponseDto(token, usuario.Nombre, usuario.Email));
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (usuario is null)
                return Unauthorized("Email o contraseña incorrectos.");

            var resultado = _passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, dto.Password);
            if (resultado == PasswordVerificationResult.Failed)
                return Unauthorized("Email o contraseña incorrectos.");

            var token = GenerarToken(usuario);
            return Ok(new AuthResponseDto(token, usuario.Nombre, usuario.Email));
        }

        private string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Name, usuario.Nombre)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(double.Parse(_config["Jwt:ExpireHours"]!)),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}