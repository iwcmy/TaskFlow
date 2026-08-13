using System.Threading;

namespace TaskFlow.Api.Models
{
    public class Proyecto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public ICollection<MiembroProyecto> Miembros { get; set; } = new List<MiembroProyecto>();
        public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
}