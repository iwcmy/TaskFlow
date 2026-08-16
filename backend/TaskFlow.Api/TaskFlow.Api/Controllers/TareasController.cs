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
    [Route("api/proyectos/{proyectoId}/tareas")]
    [Authorize]
    public class TareasController : ControllerBase
    {
        private readonly TaskFlowDbContext _context;

        public TareasController(TaskFlowDbContext context)
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
        public async Task<ActionResult<List<TareaDto>>> Listar(int proyectoId)
        {
            var miembro = await ObtenerMembresia(proyectoId, UsuarioActualId);
            if (miembro is null)
                return NotFound("Proyecto no encontrado o no pertenecés a él.");

            var tareas = await _context.Tareas
                .Where(t => t.ProyectoId == proyectoId)
                .Select(t => new TareaDto(
                    t.Id, t.Titulo, t.Descripcion, t.Estado.ToString(),
                    t.AsignadoAUsuarioId,
                    t.AsignadoAUsuario != null ? t.AsignadoAUsuario.Nombre : null))
                .ToListAsync();

            return Ok(tareas);
        }

        [HttpPost]
        public async Task<ActionResult<TareaDto>> Crear(int proyectoId, CrearTareaDto dto)
        {
            var miembro = await ObtenerMembresia(proyectoId, UsuarioActualId);
            if (miembro is null)
                return NotFound("Proyecto no encontrado o no pertenecés a él.");

            if (dto.AsignadoAUsuarioId is not null)
            {
                var asignadoEsMiembro = await ObtenerMembresia(proyectoId, dto.AsignadoAUsuarioId.Value);
                if (asignadoEsMiembro is null)
                    return BadRequest("El usuario asignado no es miembro del proyecto.");
            }

            var tarea = new Tarea
            {
                ProyectoId = proyectoId,
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                AsignadoAUsuarioId = dto.AsignadoAUsuarioId
            };

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            return Ok(new TareaDto(tarea.Id, tarea.Titulo, tarea.Descripcion, tarea.Estado.ToString(), tarea.AsignadoAUsuarioId, null));
        }

        [HttpPatch("{tareaId}/estado")]
        public async Task<IActionResult> ActualizarEstado(int proyectoId, int tareaId, ActualizarEstadoTareaDto dto)
        {
            var miembro = await ObtenerMembresia(proyectoId, UsuarioActualId);
            if (miembro is null)
                return NotFound("Proyecto no encontrado o no pertenecés a él.");

            var tarea = await _context.Tareas
                .FirstOrDefaultAsync(t => t.Id == tareaId && t.ProyectoId == proyectoId);
            if (tarea is null)
                return NotFound("Tarea no encontrada.");

            tarea.Estado = dto.Estado;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}