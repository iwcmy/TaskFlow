import { Routes, Route } from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import ProyectosPage from "./pages/ProyectosPage";

function App() {
  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route path="/proyectos" element={<ProyectosPage />} />
    </Routes>
  );
}

export default App;