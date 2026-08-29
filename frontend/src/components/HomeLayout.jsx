import { useNavigate } from "react-router-dom";
import { CheckCircle } from "lucide-react";
function HomeLayout({ children }) {
  const navigate = useNavigate();

  return (
    <div className="min-h-screen bg-surface text-text font-sans">
      <header className="border-b border-border px-10 py-4 flex items-center justify-between">
        <div className="flex items-center gap-10">
          <div className="flex items-center gap-2">
            <CheckCircle size={28} strokeWidth={2.5} className="text-primary" />
            <span className="font-mono text-primary text-3xl font-bold tracking-tight">
              TaskFlow
            </span>
          </div>

          <nav className="flex items-center gap-7">
            <a
              href="#features"
              className="group relative text-sm text-text hover:text-primary hover:-translate-y-0.5 transition-all duration-200"
            >
              Funciones
              <span className="absolute left-0 -bottom-1 h-px w-full bg-primary scale-x-0 group-hover:scale-x-100 origin-left transition-transform duration-200" />
            </a>
            <a
              href="#solutions"
              className="group relative text-sm text-text-muted hover:text-primary hover:-translate-y-0.5 transition-all duration-200"
            >
              Soluciones
              <span className="absolute left-0 -bottom-1 h-px w-full bg-primary scale-x-0 group-hover:scale-x-100 origin-left transition-transform duration-200" />
            </a>
            <a
              href="#pricing"
              className="group relative text-sm text-text-muted hover:text-primary hover:-translate-y-0.5 transition-all duration-200"
            >
              Precios
              <span className="absolute left-0 -bottom-1 h-px w-full bg-primary scale-x-0 group-hover:scale-x-100 origin-left transition-transform duration-200" />
            </a>
          </nav>
        </div>

        <div className="flex items-center gap-4">
          <button
            onClick={() => navigate("/login")}
            className="bg-primary hover:bg-primary-hover text-ink text-sm font-medium rounded-md px-4 py-2 transition-colors duration-300"
          >
            Iniciar sesión
          </button>
          <button
            onClick={() => navigate("/login")}
            className="bg-primary hover:bg-primary-hover text-ink text-sm font-medium rounded-md px-4 py-2 transition-colors duration-300"
          >
            Get Started →
          </button>
        </div>
      </header>

      <main>{children}</main>
      <footer className="bg-surface border-t border-border px-10 py-8 flex items-center justify-between">
        <span className="font-mono text-primary font-bold">TaskFlow</span>
        <div className="flex items-center gap-6 text-sm text-text-muted">
          <a
            href="#"
            className="group relative hover:text-primary hover:-translate-y-0.5 transition-all duration-200"
          >
            Privacy Policy
            <span className="absolute left-0 -bottom-0.5 h-px w-full bg-primary scale-x-0 group-hover:scale-x-100 origin-left transition-transform duration-200" />
          </a>
          <a
            href="#"
            className="group relative hover:text-primary hover:-translate-y-0.5 transition-all duration-200"
          >
            Terms of Service
            <span className="absolute left-0 -bottom-0.5 h-px w-full bg-primary scale-x-0 group-hover:scale-x-100 origin-left transition-transform duration-200" />
          </a>
          <a
            href="#"
            className="group relative hover:text-primary hover:-translate-y-0.5 transition-all duration-200"
          >
            Contacto
            <span className="absolute left-0 -bottom-0.5 h-px w-full bg-primary scale-x-0 group-hover:scale-x-100 origin-left transition-transform duration-200" />
          </a>
        </div>
        <span className="text-sm text-text-muted">
          © 2026 TaskFlow. All rights reserved.
        </span>
      </footer>
    </div>
  );
}

export default HomeLayout;
