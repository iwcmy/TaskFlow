using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskFlow.Api.Data;
using TaskFlow.Api.Dtos;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProyectosController : ControllerBase
    {
        private readonly TaskFlowDbContext _context;

        public ProyectosController(TaskFlowDbContext context)
        {
            _context = context;
        }

        private int UsuarioActualId =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

           private async Task<MiembroProyecto?> ObtenerMembresia(int proyectoId, int usuarioId)
{
    return await _context.MiembrosProyecto
        .FirstOrDefaultAsync(mp => mp.ProyectoId == proyectoId && mp.UsuarioId == usuarioId);
}

        [HttpGet]
        public async Task<ActionResult<List<ProyectoDto>>> Listar()
        {
            var usuarioId = UsuarioActualId;

            var proyectos = await _context.MiembrosProyecto
                .Where(mp => mp.UsuarioId == usuarioId)
                .Select(mp => new ProyectoDto(
                    mp.Proyecto.Id,
                    mp.Proyecto.Nombre,
                    mp.Proyecto.Descripcion,
                    mp.Rol.ToString()))
                .ToListAsync();

            return Ok(proyectos);
        }

        [HttpPost]
        public async Task<ActionResult<ProyectoDto>> Crear(CrearProyectoDto dto)
        {
            var usuarioId = UsuarioActualId;

            var proyecto = new Proyecto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            proyecto.Miembros.Add(new MiembroProyecto
            {
                UsuarioId = usuarioId,
                Rol = RolProyecto.Admin
            });

            _context.Proyectos.Add(proyecto);
            await _context.SaveChangesAsync();

            return Ok(new ProyectoDto(proyecto.Id, proyecto.Nombre, proyecto.Descripcion, RolProyecto.Admin.ToString()));
        }

        [HttpPost("{proyectoId}/miembros")]
        public async Task<IActionResult> AgregarMiembro(int proyectoId, AgregarMiembroDto dto)
        {
            var usuarioActualId = UsuarioActualId;

            var miembroActual = await ObtenerMembresia(proyectoId, UsuarioActualId);

            if (miembroActual is null)
                return NotFound("Proyecto no encontrado o no pertenecés a él.");

            if (miembroActual.Rol != RolProyecto.Admin)
                return Forbid();

            var usuarioAAgregar = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.EmailUsuario);

            if (usuarioAAgregar is null)
                return BadRequest("No existe un usuario con ese email.");

            var yaEsMiembro = await _context.MiembrosProyecto
                .AnyAsync(mp => mp.ProyectoId == proyectoId && mp.UsuarioId == usuarioAAgregar.Id);

            if (yaEsMiembro)
                return BadRequest("Ese usuario ya es miembro del proyecto.");

            _context.MiembrosProyecto.Add(new MiembroProyecto
            {
                ProyectoId = proyectoId,
                UsuarioId = usuarioAAgregar.Id,
                Rol = dto.Rol
            });

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{proyectoId}")]
        public async Task<IActionResult> Editar(int proyectoId, CrearProyectoDto dto)
        {
            var miembroActual = await ObtenerMembresia(proyectoId, UsuarioActualId);

            if (miembroActual is null)
                return NotFound("Proyecto no encontrado o no pertenecés a él.");

            if (miembroActual.Rol != RolProyecto.Admin)
                return Forbid();

            var proyecto = await _context.Proyectos.FindAsync(proyectoId);
            if (proyecto is null)
                return NotFound();

            proyecto.Nombre = dto.Nombre;
            proyecto.Descripcion = dto.Descripcion;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{proyectoId}")]
        public async Task<IActionResult> Eliminar(int proyectoId)
        {
            var miembroActual = await ObtenerMembresia(proyectoId, UsuarioActualId);

            if (miembroActual is null)
                return NotFound("Proyecto no encontrado o no pertenecés a él.");

            if (miembroActual.Rol != RolProyecto.Admin)
                return Forbid();

            var proyecto = await _context.Proyectos.FindAsync(proyectoId);
            if (proyecto is null)
                return NotFound();

            _context.Proyectos.Remove(proyecto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }


}