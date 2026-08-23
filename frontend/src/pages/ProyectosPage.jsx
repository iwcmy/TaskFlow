import { useState, useEffect } from "react";
import { useNavigate, Link } from "react-router-dom";
import { obtenerProyectos, crearProyecto } from "../services/proyectosService";

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
      <h2>Mis proyectos</h2>
      {error && <p style={{ color: "red" }}>{error}</p>}

      <ul>
        {proyectos.map((proyecto) => (
          <li key={proyecto.id}>
            <Link to={`/proyectos/${proyecto.id}`}>
              {proyecto.nombre} — {proyecto.rolDelUsuario}
            </Link>
          </li>
        ))}
      </ul>

      <h3>Crear nuevo proyecto</h3>
      <form onSubmit={manejarCrearProyecto}>
        <div>
          <label>Nombre</label>
          <input
            type="text"
            value={nombre}
            onChange={(e) => setNombre(e.target.value)}
          />
        </div>
        <div>
          <label>Descripción</label>
          <input
            type="text"
            value={descripcion}
            onChange={(e) => setDescripcion(e.target.value)}
          />
        </div>
        <button type="submit">Crear</button>
      </form>
    </div>
  );
}

export default ProyectosPage;
