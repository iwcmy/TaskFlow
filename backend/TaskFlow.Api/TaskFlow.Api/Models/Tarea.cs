namespace TaskFlow.Api.Models
{
    public enum EstadoTarea
    {
        Pendiente,
        EnProgreso,
        Hecha
    }

    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public EstadoTarea Estado { get; set; } = EstadoTarea.Pendiente;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public int ProyectoId { get; set; }
        public Proyecto Proyecto { get; set; } = null!;

        public int? AsignadoAUsuarioId { get; set; }
        public Usuario? AsignadoAUsuario { get; set; }

        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}