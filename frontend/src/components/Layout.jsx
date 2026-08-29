import { useNavigate, useLocation, Link } from "react-router-dom";
import { LayoutGrid, FolderKanban, ListTodo, Settings, HelpCircle, LogOut } from "lucide-react";

function Layout({ children }) {
  const navigate = useNavigate();
  const location = useLocation();

  function manejarLogout() {
    localStorage.removeItem("token");
    navigate("/login");
  }

  const esProyectos = location.pathname.startsWith("/proyectos");

  return (
    <div className="min-h-screen bg-ink text-text font-sans flex">
      <aside className="w-64 border-r border-border flex flex-col justify-between px-4 py-6">
        <div>
          <div className="flex items-center gap-2 px-2 mb-8">
            <div className="w-8 h-8 rounded-md bg-primary/10 flex items-center justify-center">
              <FolderKanban size={16} className="text-primary" />
            </div>
            <div>
              <p className="font-mono text-primary font-bold leading-tight">TaskFlow</p>
              <p className="text-xs text-text-muted leading-tight">Workspace</p>
            </div>
          </div>

          <nav className="space-y-1">
            <span className="flex items-center gap-3 px-3 py-2 rounded-md text-sm text-text-muted opacity-40 cursor-not-allowed">
              <LayoutGrid size={18} />
              Dashboard
            </span>
            <Link
              to="/proyectos"
              className={`flex items-center gap-3 px-3 py-2 rounded-md text-sm transition-colors ${
                esProyectos
                  ? "bg-primary/10 text-primary font-medium"
                  : "text-text-muted hover:text-text hover:bg-surface"
              }`}
            >
              <FolderKanban size={18} />
              Projects
            </Link>
            <span className="flex items-center gap-3 px-3 py-2 rounded-md text-sm text-text-muted opacity-40 cursor-not-allowed">
              <ListTodo size={18} />
              Tasks
            </span>
            <span className="flex items-center gap-3 px-3 py-2 rounded-md text-sm text-text-muted opacity-40 cursor-not-allowed">
              <Settings size={18} />
              Settings
            </span>
          </nav>
        </div>

        <div className="space-y-1 border-t border-border pt-4">
          <span className="flex items-center gap-3 px-3 py-2 rounded-md text-sm text-text-muted opacity-40 cursor-not-allowed">
            <HelpCircle size={18} />
            Help
          </span>
          <button
            onClick={manejarLogout}
            className="w-full flex items-center gap-3 px-3 py-2 rounded-md text-sm text-text-muted hover:text-text hover:bg-surface transition-colors"
          >
            <LogOut size={18} />
            Logout
          </button>
        </div>
      </aside>

      <main className="flex-1 px-10 py-10">{children}</main>
    </div>
  );
}

export default Layout;