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
    public class ProyectosController : TaskFlowControllerBase
    {
        public ProyectosController(TaskFlowDbContext context) : base(context)
        {
        }


        [HttpGet]
        public async Task<ActionResult<List<ProyectoDto>>> Listar()
        {
            var usuarioId = UsuarioActualId;

            var proyectos = await Context.MiembrosProyecto
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

            Context.Proyectos.Add(proyecto);
            await Context.SaveChangesAsync();

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

            var usuarioAAgregar = await Context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.EmailUsuario);

            if (usuarioAAgregar is null)
                return BadRequest("No existe un usuario con ese email.");

            var yaEsMiembro = await Context.MiembrosProyecto
                .AnyAsync(mp => mp.ProyectoId == proyectoId && mp.UsuarioId == usuarioAAgregar.Id);

            if (yaEsMiembro)
                return BadRequest("Ese usuario ya es miembro del proyecto.");

            Context.MiembrosProyecto.Add(new MiembroProyecto
            {
                ProyectoId = proyectoId,
                UsuarioId = usuarioAAgregar.Id,
                Rol = dto.Rol
            });

            await Context.SaveChangesAsync();
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

            var proyecto = await Context.Proyectos.FindAsync(proyectoId);
            if (proyecto is null)
                return NotFound();

            proyecto.Nombre = dto.Nombre;
            proyecto.Descripcion = dto.Descripcion;
            await Context.SaveChangesAsync();

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

            var proyecto = await Context.Proyectos.FindAsync(proyectoId);
            if (proyecto is null)
                return NotFound();

            Context.Proyectos.Remove(proyecto);
            await Context.SaveChangesAsync();

            return NoContent();
        }

    }


}