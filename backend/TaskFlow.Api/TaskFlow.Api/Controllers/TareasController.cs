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
    public class TareasController : TaskFlowControllerBase
    {
        public TareasController(TaskFlowDbContext context) : base(context)
        {
        }


        [HttpGet]
        public async Task<ActionResult<List<TareaDto>>> Listar(int proyectoId)
        {
            var miembro = await ObtenerMembresia(proyectoId, UsuarioActualId);
            if (miembro is null)
                return NotFound("Proyecto no encontrado o no pertenecés a él.");

            var tareas = await Context.Tareas
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

            Context.Tareas.Add(tarea);
            await Context.SaveChangesAsync();

            return Ok(new TareaDto(tarea.Id, tarea.Titulo, tarea.Descripcion, tarea.Estado.ToString(), tarea.AsignadoAUsuarioId, null));
        }

        [HttpPatch("{tareaId}/estado")]
        public async Task<IActionResult> ActualizarEstado(int proyectoId, int tareaId, ActualizarEstadoTareaDto dto)
        {
            var miembro = await ObtenerMembresia(proyectoId, UsuarioActualId);
            if (miembro is null)
                return NotFound("Proyecto no encontrado o no pertenecés a él.");

            var tarea = await Context.Tareas
                .FirstOrDefaultAsync(t => t.Id == tareaId && t.ProyectoId == proyectoId);
            if (tarea is null)
                return NotFound("Tarea no encontrada.");

            tarea.Estado = dto.Estado;
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}