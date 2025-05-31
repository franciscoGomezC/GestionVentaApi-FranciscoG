using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVentas.core.DTOs
{
    public class VentaCreateDTO
    {
        public int ClienteId { get; set; }
        public int EmpleadoId { get; set; }
        public string MetodoPago { get; set; }
        public List<DetalleVentaCreateDTO> DetallesVenta { get; set; } //un listado del dto de los detalles de la venta
    }
}
