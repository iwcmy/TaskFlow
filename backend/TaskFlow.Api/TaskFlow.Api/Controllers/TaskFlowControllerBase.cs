using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskFlow.Api.Data;
using TaskFlow.Api.Models;

namespace TaskFlow.Api.Controllers
{
    [Authorize]
    public abstract class TaskFlowControllerBase : ControllerBase
    {
        protected readonly TaskFlowDbContext Context;

        protected TaskFlowControllerBase(TaskFlowDbContext context)
        {
            Context = context;
        }

        protected int UsuarioActualId =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        protected async Task<MiembroProyecto?> ObtenerMembresia(int proyectoId, int usuarioId)
        {
            return await Context.MiembrosProyecto
                .FirstOrDefaultAsync(mp => mp.ProyectoId == proyectoId && mp.UsuarioId == usuarioId);
        }
    }
}