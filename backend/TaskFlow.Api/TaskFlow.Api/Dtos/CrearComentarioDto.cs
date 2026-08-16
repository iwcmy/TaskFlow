using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Api.Dtos
{
    public record CrearComentarioDto(
        [Required(ErrorMessage = "El comentario no puede estar vacío")]
        [StringLength(1000, MinimumLength = 1)]
        string Contenido);
}