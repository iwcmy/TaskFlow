import { useParams } from "react-router-dom";

function ProyectoDetallePage() {
  const { id } = useParams();

  return (
    <div>
      <h2>Detalle del proyecto {id}</h2>
    </div>
  );
}

export default ProyectoDetallePage;