using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVentas.core.DTOs
{
    public class VentaDTO
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string ClienteNombre { get; set; }
        public int ClienteId { get; set; } //esto se añado para poder obtener el id del cliente
        public string MetodoPago { get; set; }
        public List<DetalleVentaDTO> DetallesVenta { get; set; } //un listado del dto de los detalles de la venta
    }
}
