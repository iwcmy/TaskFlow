import { useState, useEffect } from "react";
import { obtenerProyectos } from "../services/proyectosService";

function ProyectosPage() {
  const [proyectos, setProyectos] = useState([]);
  const [error, setError] = useState("");

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

  return (
    <div>
      <h2>Mis proyectos</h2>
      {error && <p style={{ color: "red" }}>{error}</p>}
      <ul>
        {proyectos.map((proyecto) => (
          <li key={proyecto.id}>
            {proyecto.nombre} — {proyecto.descripcion} —{proyecto.rolDelUsuario} 
          </li>
        ))}
      </ul>
    </div>
  );
}

export default ProyectosPage;