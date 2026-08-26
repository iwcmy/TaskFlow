import { useNavigate } from "react-router-dom";

function Layout({ children }) {
  const navigate = useNavigate();

  function manejarLogout() {
    localStorage.removeItem("token");
    navigate("/login");
  }

  return (
    <div className="min-h-screen bg-ink text-text font-sans">
      <header className="border-b border-border px-6 py-4 flex items-center justify-between">
        <span className="font-mono text-primary text-lg tracking-tight">
          TaskFlow
        </span>
        <button
          onClick={manejarLogout}
          className="text-sm text-text-muted hover:text-text transition-colors"
        >
          Cerrar sesión
        </button>
      </header>
      <main className="max-w-4xl mx-auto px-6 py-8">{children}</main>
    </div>
  );
}

export default Layout;