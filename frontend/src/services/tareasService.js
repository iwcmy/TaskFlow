const API_URL = "https://localhost:7040/api"; // tu puerto real

export async function obtenerTareas(proyectoId) {
  const token = localStorage.getItem("token");

  const respuesta = await fetch(`${API_URL}/proyectos/${proyectoId}/tareas`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!respuesta.ok) {
    throw new Error("No se pudieron cargar las tareas");
  }

  return await respuesta.json();
}

export async function crearTarea(proyectoId, titulo, descripcion) {
  const token = localStorage.getItem("token");

  const respuesta = await fetch(`${API_URL}/proyectos/${proyectoId}/tareas`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify({ titulo, descripcion, asignadoAUsuarioId: null }),
  });

  if (!respuesta.ok) {
    throw new Error("No se pudo crear la tarea");
  }

  return await respuesta.json();
}