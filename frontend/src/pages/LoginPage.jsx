import { useState } from "react";
import { login } from "../services/authService";
import { useNavigate } from "react-router-dom";
import { Mail, Lock, CheckCircle } from "lucide-react";

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
    <div className="min-h-screen bg-ink flex items-center justify-center px-4">
      <div className="w-full max-w-md">
        <div className="mb-8 text-center">
          <div className="flex items-center justify-center gap-2">
            <CheckCircle className="text-primary" size={45} strokeWidth={2} />
            <span className="font-mono text-primary text-7xl font-black tracking-tight">
              TaskFlow
            </span>
          </div>

          <p className="text-text-muted text-lg mt-3">
            Inicia sesión para ver tus proyectos
          </p>
        </div>

        <form
          onSubmit={manejarSubmit}
          className="border border-border rounded-lg p-10 space-y-4"
        >
          <div>
            <label className="block text-sm text-text-muted mb-1">Email</label>

            <div className="relative">
              <Mail
                size={18}
                className="absolute left-3 top-1/2 -translate-y-1/2 text-text-muted"
              />

              <input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="email@gmail.com"
                className="w-full bg-surface border border-border px-3 py-2 pl-10 text-text focus:outline-none focus:border-primary transition-colors"
              />
            </div>
          </div>

          <div>
            <label className="block text-sm text-text-muted mb-1">
              Contraseña
            </label>

            <div className="relative">
              <Lock
                size={18}
                className="absolute left-3 top-1/2 -translate-y-1/2 text-text-muted"
              />

              <input
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="********"
                className="w-full bg-surface border border-border px-3 py-2 pl-10 text-text focus:outline-none focus:border-primary transition-colors"
              />
            </div>
          </div>

          {error && (
            <p className="text-sm text-accent bg-accent/10 border border-accent/30 rounded-md px-3 py-2">
              {error}
            </p>
          )}

          <button
            type="submit"
            className="w-full bg-primary hover:bg-primary-hover text-ink font-medium rounded-md py-2 transition-colors"
          >
            Ingresar
          </button>
        </form>
      </div>
    </div>
  );
}

export default LoginPage;
