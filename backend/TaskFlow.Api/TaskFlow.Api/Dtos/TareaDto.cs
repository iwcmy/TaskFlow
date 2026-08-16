namespace TaskFlow.Api.Dtos
{
    public record TareaDto(
        int Id,
        string Titulo,
        string? Descripcion,
        string Estado,
        int? AsignadoAUsuarioId,
        string? AsignadoAUsuarioNombre);
}