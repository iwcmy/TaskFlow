import { useState } from "react";
import { login } from "../services/authService";
import { useNavigate } from "react-router-dom";


function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  
  async function manejarSubmit(evento) {
    evento.preventDefault();
    setError("");

    try {
      const resultado = await login(email, password);
      localStorage.setItem("token", resultado.token);
      navigate("/proyectos");
    } catch (err) {
      setError(err.message);
    }
  }

  return (
    <div>
      <h2>Iniciar sesión</h2>
      <form onSubmit={manejarSubmit}>
        <div>
          <label>Email</label>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>
        <div>
          <label>Contraseña</label>
          <input
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>
        {error && <p style={{ color: "red" }}>{error}</p>}
        <button type="submit">Ingresar</button>
      </form>
    </div>
  );
}

export default LoginPage;