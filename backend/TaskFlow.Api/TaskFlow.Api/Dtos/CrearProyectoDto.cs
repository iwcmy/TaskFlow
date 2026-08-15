using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Api.Dtos
{
    public record CrearProyectoDto(
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        string Nombre,

        [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres")]
        string? Descripcion
    );
}