import { Routes, Route } from "react-router-dom";
import Layout from "./components/Layout";
import LoginPage from "./pages/LoginPage";
import ProyectosPage from "./pages/ProyectosPage";
import ProyectoDetallePage from "./pages/ProyectoDetallePage";

function App() {
  return (
    <Routes>
      <Route path="/login" element={<LoginPage />} />
      <Route
        path="/proyectos"
        element={
          <Layout>
            <ProyectosPage />
          </Layout>
        }
      />
      <Route
        path="/proyectos/:id"
        element={
          <Layout>
            <ProyectoDetallePage />
          </Layout>
        }
      />
    </Routes>
  );
}

export default App;
