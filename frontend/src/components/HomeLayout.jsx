import { useNavigate } from "react-router-dom";

function HomeLayout({ children }) {
  const navigate = useNavigate();

  return (
    <div className="min-h-screen bg-ink text-text font-sans">
      <header className="border-b border-border px-10 py-4 flex items-center justify-between">
        <div className="flex items-center gap-10">
          <span className="font-mono text-primary text-2xl font-bold tracking-tight">
            TaskFlow
          </span>

          <nav className="flex items-center gap-7">
            <a
              href="#features"
              className="text-sm text-text hover:text-primary transition-colors"
            >
              Features
            </a>
            <a
              href="#solutions"
              className="text-sm text-text-muted hover:text-text transition-colors"
            >
              Solutions
            </a>
            <a
              href="#pricing"
              className="text-sm text-text-muted hover:text-text transition-colors"
            >
              Pricing
            </a>
          </nav>
        </div>

        <div className="flex items-center gap-4">
          <button
            onClick={() => navigate("/login")}
            className="text-sm text-text-muted hover:text-text transition-colors"
          >
            Log In
          </button>
          <button
            onClick={() => navigate("/login")}
            className="bg-primary hover:bg-primary-hover text-ink text-sm font-medium rounded-md px-4 py-2 transition-colors"
          >
            Get Started →
          </button>
        </div>
      </header>

      <main>{children}</main>
    </div>
  );
}

export default HomeLayout;
