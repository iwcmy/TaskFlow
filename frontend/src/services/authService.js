const API_URL = "https://localhost:7040/api"; 

export async function login(email, password) {
  const respuesta = await fetch(`${API_URL}/auth/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ email, password }),
  });

  if (!respuesta.ok) {
    throw new Error("Email o contraseña incorrectos");
  }

  return await respuesta.json();
}