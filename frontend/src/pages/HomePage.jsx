import { PlayCircle, Users, BarChart3, Zap } from "lucide-react";

function HomePage() {
  return (
    <div className="px-10 py-20 text-center bg-ink">
      <span className="inline-flex items-center gap-2 text-xs font-mono text-primary bg-primary/10 border border-primary/30 rounded-full px-3 py-1">
        <span className="w-1.5 h-1.5 rounded-full bg-primary" />
        New: TaskFlow Automation 2.0
      </span>

      <h1 className="text-5xl font-bold mt-6 leading-tight max-w-3xl mx-auto">
        Maneja el trabajo de tu equipo,
        <br />
        proyectos y tareas online.
      </h1>

      <p className="text-text-muted text-lg mt-6 max-w-2xl mx-auto">
        El centro centralizado donde la estrategia se convierte en ejecución.
        <br />
        Alinea a tus equipos, sigue el progreso en tiempo real y automatiza las
        tareas repetitivas de tu día a día.
      </p>

      <div className="flex items-center justify-center gap-4 mt-8">
        <button className="bg-primary hover:bg-primary-hover text-ink font-medium rounded-md px-6 py-3 transition-colors duration-300">
          Get Started Free
        </button>
        <button className="flex items-center gap-2 border border-border hover:bg-surface hover:border-text-muted text-text rounded-md px-6 py-3 transition-colors duration-300">
          <PlayCircle size={18} />
          Ver demo
        </button>
      </div>

      <div className="mt-16 max-w-7xl mx-auto border border-border rounded-lg bg-surface aspect-video flex items-center justify-center">
        <span className="text-text-muted font-mono text-sm">
          [ product preview ]
        </span>
      </div>

      <div className="max-w-6xl mx-auto mt-32 text-left ">
        <h2 className="text-3xl font-bold">
          Diseñado para equipos de alto rendimiento.
        </h2>
        <p className="text-text-muted mt-2">
          Todo lo que necesitas para coordinar flujos de trabajo complejos.
        </p>

        <div className="grid md:grid-cols-2 gap-6 mt-10">
          <div className="border border-border rounded-lg p-6">
            <div className="w-10 h-10 rounded-md bg-primary/10 flex items-center justify-center mb-4">
              <Users size={20} className="text-primary" />
            </div>
            <h3 className="font-semibold text-lg">
              Colaboración sin fricciones
            </h3>
            <p className="text-text-muted text-sm mt-2">
              Conecta a tu equipo en distintas zonas horarias con
              actualizaciones en tiempo real y herramientas de comunicación
              integradas.
            </p>
          </div>

          <div className="border border-border rounded-lg p-6">
            <div className="w-10 h-10 rounded-md bg-primary/10 flex items-center justify-center mb-4">
              <BarChart3 size={20} className="text-primary" />
            </div>
            <h3 className="font-semibold text-lg">Seguimiento preciso</h3>
            <p className="text-text-muted text-sm mt-2">
              Supervisa el estado de tus proyectos de un vistazo con paneles
              personalizables y gráficos de avance de los sprints.
            </p>
          </div>

          <div className="border border-border rounded-lg p-6 md:col-span-2 flex flex-col md:flex-row items-center gap-8">
            <div className="flex-1">
              <div className="w-10 h-10 rounded-md bg-accent/10 flex items-center justify-center mb-4">
                <Zap size={20} className="text-accent" />
              </div>
              <h3 className="font-semibold text-lg">Powerful Automation</h3>
              <p className="text-text-muted text-sm mt-2 max-w-md">
                Set triggers, design complex logic flows, and let TaskFlow
                handle repetitive tasks so you can focus on deep work.
              </p>
            </div>

            <div className="flex-1 w-full">
              <div className="border border-border rounded-lg bg-surface aspect-video flex items-center justify-center">
                <span className="text-text-muted font-mono text-sm">
                  [ automation preview ]
                </span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default HomePage;
