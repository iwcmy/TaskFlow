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
    }
}