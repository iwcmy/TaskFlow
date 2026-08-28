import { PlayCircle } from "lucide-react";

function HomePage() {
  return (
    <div className="px-10 py-20 text-center">
      <span className="inline-flex items-center gap-2 text-xs font-mono text-primary bg-primary/10 border border-primary/30 rounded-full px-3 py-1">
        <span className="w-1.5 h-1.5 rounded-full bg-primary" />
        New: TaskFlow Automation 2.0
      </span>

      <h1 className="text-5xl font-bold mt-6 leading-tight max-w-3xl mx-auto">
        Manage your team's work,
        <br />
        projects, and tasks online.
      </h1>

      <p className="text-text-muted text-lg mt-6 max-w-2xl mx-auto">
        The centralized hub where strategy meets execution. Align your
        teams, track progress in real-time, and automate the busywork out
        of your day.
      </p>

      <div className="flex items-center justify-center gap-4 mt-8">
        <button className="bg-primary hover:bg-primary-hover text-ink font-medium rounded-md px-6 py-3 transition-colors">
          Get Started Free
        </button>
        <button className="flex items-center gap-2 border border-border hover:border-text-muted text-text rounded-md px-6 py-3 transition-colors">
          <PlayCircle size={18} />
          Watch Demo
        </button>
      </div>

      <div className="mt-16 max-w-7xl mx-auto border border-border rounded-lg bg-surface aspect-video flex items-center justify-center">
        <span className="text-text-muted font-mono text-sm">
          [ product preview ]
        </span>
      </div>
    </div>
  );
}

export default HomePage;