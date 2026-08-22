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