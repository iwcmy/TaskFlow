namespace TaskFlow.Api.Models
{
    public enum RolProyecto
    {
        Admin,
        Miembro
    }

    public class MiembroProyecto
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public int ProyectoId { get; set; }
        public Proyecto Proyecto { get; set; } = null!;

        public RolProyecto Rol { get; set; }
    }
}