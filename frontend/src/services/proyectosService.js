const API_URL = "https://localhost:7040/api";

export async function obtenerProyectos() {
  const token = localStorage.getItem("token");

  const respuesta = await fetch(`${API_URL}/proyectos`, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  if (!respuesta.ok) {
    throw new Error("No se pudieron cargar los proyectos");
  }

  return await respuesta.json();
}

export async function crearProyecto(nombre, descripcion) {
  const token = localStorage.getItem("token");

  const respuesta = await fetch(`${API_URL}/proyectos`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify({ nombre, descripcion }),
  });

  if (!respuesta.ok) {
    throw new Error("No se pudo crear el proyecto");
  }

  return await respuesta.json();
}