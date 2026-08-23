import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import { obtenerTareas, crearTarea } from "../services/tareasService";

function ProyectoDetallePage() {
  const { id } = useParams();
  const [tareas, setTareas] = useState([]);
  const [error, setError] = useState("");
  const [titulo, setTitulo] = useState("");

  useEffect(() => {
    async function cargarTareas() {
      try {
        const datos = await obtenerTareas(id);
        setTareas(datos);
      } catch (err) {
        setError(err.message);
      }
    }

    cargarTareas();
  }, [id]);

  async function manejarCrearTarea(evento) {
    evento.preventDefault();
    setError("");

    try {
      const nuevaTarea = await crearTarea(id, titulo, "");
      setTareas([...tareas, nuevaTarea]);
      setTitulo("");
    } catch (err) {
      setError(err.message);
    }
  }

  return (
    <div>
      <h2>Tareas del proyecto</h2>
      {error && <p style={{ color: "red" }}>{error}</p>}

      <ul>
        {tareas.map((tarea) => (
          <li key={tarea.id}>
            {tarea.titulo} — {tarea.estado}
          </li>
        ))}
      </ul>

      <form onSubmit={manejarCrearTarea}>
        <input
          type="text"
          placeholder="Título de la tarea"
          value={titulo}
          onChange={(e) => setTitulo(e.target.value)}
        />
        <button type="submit">Agregar tarea</button>
      </form>
    </div>
  );
}

export default ProyectoDetallePage;