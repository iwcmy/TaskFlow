using TaskFlow.Api.Models;

namespace TaskFlow.Api.Dtos
{
    public record AgregarMiembroDto(string EmailUsuario, RolProyecto Rol);
}