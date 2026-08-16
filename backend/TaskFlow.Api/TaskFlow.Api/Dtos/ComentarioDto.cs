namespace TaskFlow.Api.Dtos
{
    public record ComentarioDto(
        int Id,
        string Contenido,
        DateTime FechaCreacion,
        int UsuarioId,
        string UsuarioNombre);
}