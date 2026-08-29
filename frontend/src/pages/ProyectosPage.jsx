import { useState, useEffect } from "react";
import { useNavigate, Link } from "react-router-dom";
import { obtenerProyectos, crearProyecto } from "../services/proyectosService";
import { Plus, FolderKanban } from "lucide-react";

function ProyectosPage() {
  const [proyectos, setProyectos] = useState([]);
  const [error, setError] = useState("");
  const [nombre, setNombre] = useState("");
  const [descripcion, setDescripcion] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (!token) {
      navigate("/login");
    }
  }, [navigate]);

  useEffect(() => {
    async function cargarProyectos() {
      try {
        const datos = await obtenerProyectos();
        setProyectos(datos);
      } catch (err) {
        setError(err.message);
      }
    }

    cargarProyectos();
  }, []);

  async function manejarCrearProyecto(evento) {
    evento.preventDefault();
    setError("");

    try {
      const nuevoProyecto = await crearProyecto(nombre, descripcion);
      setProyectos([...proyectos, nuevoProyecto]);
      setNombre("");
      setDescripcion("");
    } catch (err) {
      setError(err.message);
    }
  }

  return (
    <div>
      <div className="flex items-center justify-between mb-8">
        <div>
          <h1 className="text-3xl font-bold">Mis proyectos</h1>
          <p className="text-text-muted mt-1">
            Manage and track your active initiatives.
          </p>
        </div>
      </div>

      {error && (
        <p className="text-sm text-accent bg-accent/10 border border-accent/30 rounded-md px-3 py-2 mb-6">
          {error}
        </p>
      )}

      <div className="grid md:grid-cols-2 gap-6 mb-10">
        {proyectos.map((proyecto) => (
          <Link
            key={proyecto.id}
            to={`/proyectos/${proyecto.id}`}
            className="border border-border rounded-lg p-6 hover:border-primary/50 transition-colors"
          >
            <span
              className={`inline-flex text-xs font-mono uppercase tracking-wide rounded-full px-3 py-1 border ${
                proyecto.rolDelUsuario === "Admin"
                  ? "text-primary bg-primary/10 border-primary/30"
                  : "text-text-muted bg-surface border-border"
              }`}
            >
              {proyecto.rolDelUsuario}
            </span>

            <h3 className="font-semibold text-lg mt-4">{proyecto.nombre}</h3>
            {proyecto.descripcion && (
              <p className="text-text-muted text-sm mt-2">
                {proyecto.descripcion}
              </p>
            )}
          </Link>
        ))}
      </div>

      <div className="border border-border rounded-lg p-6 max-w-lg">
        <div className="flex items-center gap-2 mb-4">
          <Plus size={18} className="text-primary" />
          <h3 className="font-semibold">New Project</h3>
        </div>

        <form onSubmit={manejarCrearProyecto} className="space-y-4">
          <div>
            <label className="block text-sm text-text-muted mb-1">Nombre</label>
            <input
              type="text"
              value={nombre}
              onChange={(e) => setNombre(e.target.value)}
              className="w-full bg-ink border border-border rounded-md px-3 py-2 text-text focus:outline-none focus:border-primary transition-colors"
            />
          </div>
          <div>
            <label className="block text-sm text-text-muted mb-1">
              Descripción
            </label>
            <input
              type="text"
              value={descripcion}
              onChange={(e) => setDescripcion(e.target.value)}
              className="w-full bg-ink border border-border rounded-md px-3 py-2 text-text focus:outline-none focus:border-primary transition-colors"
            />
          </div>
          <button
            type="submit"
            className="bg-primary hover:bg-primary-hover text-ink font-medium rounded-md px-4 py-2 transition-colors"
          >
            Crear proyecto
          </button>
        </form>
      </div>
    </div>
  );
}

export default ProyectosPage;
