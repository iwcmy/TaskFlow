import { Routes, Route } from "react-router-dom";

import Layout from "./components/Layout";
import HomeLayout from "./components/HomeLayout";

import LoginPage from "./pages/LoginPage";
import ProyectosPage from "./pages/ProyectosPage";
import ProyectoDetallePage from "./pages/ProyectoDetallePage";
import HomePage from "./pages/HomePage";

function App() {
  return (
    <Routes>
      {/* HOME */}
      <Route
        path="/"
        element={
          <HomeLayout>
            <HomePage />
          </HomeLayout>
        }
      />

      {/* LOGIN */}
      <Route path="/login" element={<LoginPage />} />

      {/* APP */}
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
