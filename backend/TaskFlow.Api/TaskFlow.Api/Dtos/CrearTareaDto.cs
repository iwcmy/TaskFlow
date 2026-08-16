using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Api.Dtos
{
    public record CrearTareaDto(
        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(150, MinimumLength = 3)]
        string Titulo,

        [StringLength(1000)]
        string? Descripcion,

        int? AsignadoAUsuarioId
    );
}