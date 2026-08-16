using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskFlow.Api.Data;
using TaskFlow.Api.Dtos;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Controllers
{
    [ApiController]
    [Route("api/proyectos/{proyectoId}/tareas/{tareaId}/comentarios")]
    public class ComentariosController : TaskFlowControllerBase
    {
        public ComentariosController(TaskFlowDbContext context) : base(context)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<ComentarioDto>>> Listar(int proyectoId, int tareaId)
        {
            var miembro = await ObtenerMembresia(proyectoId, UsuarioActualId);
            if (miembro is null)
                return NotFound("Proyecto no encontrado o no pertenecés a él.");

            var tareaExiste = await Context.Tareas.AnyAsync(t => t.Id == tareaId && t.ProyectoId == proyectoId);
            if (!tareaExiste)
                return NotFound("Tarea no encontrada.");

            var comentarios = await Context.Comentarios
                .Where(c => c.TareaId == tareaId)
                .OrderBy(c => c.FechaCreacion)
                .Select(c => new ComentarioDto(c.Id, c.Contenido, c.FechaCreacion, c.UsuarioId, c.Usuario.Nombre))
                .ToListAsync();

            return Ok(comentarios);
        }

        [HttpPost]
        public async Task<ActionResult<ComentarioDto>> Crear(int proyectoId, int tareaId, CrearComentarioDto dto)
        {
            var miembro = await ObtenerMembresia(proyectoId, UsuarioActualId);
            if (miembro is null)
                return NotFound("Proyecto no encontrado o no pertenecés a él.");

            var tareaExiste = await Context.Tareas.AnyAsync(t => t.Id == tareaId && t.ProyectoId == proyectoId);
            if (!tareaExiste)
                return NotFound("Tarea no encontrada.");

            var comentario = new Comentario
            {
                TareaId = tareaId,
                UsuarioId = UsuarioActualId,
                Contenido = dto.Contenido
            };

            Context.Comentarios.Add(comentario);
            await Context.SaveChangesAsync();

            var usuario = await Context.Usuarios.FindAsync(UsuarioActualId);

            return Ok(new ComentarioDto(comentario.Id, comentario.Contenido, comentario.FechaCreacion, comentario.UsuarioId, usuario!.Nombre));
        }

        [HttpDelete("{comentarioId}")]
        public async Task<IActionResult> Eliminar(int proyectoId, int tareaId, int comentarioId)
        {
            var miembro = await ObtenerMembresia(proyectoId, UsuarioActualId);
            if (miembro is null)
                return NotFound("Proyecto no encontrado o no pertenecés a él.");

            var comentario = await Context.Comentarios
                .FirstOrDefaultAsync(c => c.Id == comentarioId && c.TareaId == tareaId);
            if (comentario is null)
                return NotFound("Comentario no encontrado.");

            var esAutor = comentario.UsuarioId == UsuarioActualId;
            var esAdmin = miembro.Rol == RolProyecto.Admin;

            if (!esAutor && !esAdmin)
                return Forbid();

            Context.Comentarios.Remove(comentario);
            await Context.SaveChangesAsync();

            return NoContent();
        }
    }
}