import { Routes, Route } from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import ProyectosPage from "./pages/ProyectosPage";
import ProyectoDetallePage from "./pages/ProyectoDetallePage";

function App() {
  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route path="/proyectos" element={<ProyectosPage />} />
      <Route path="/proyectos/:id" element={<ProyectoDetallePage />} />
    </Routes>
  );
}

export default App;