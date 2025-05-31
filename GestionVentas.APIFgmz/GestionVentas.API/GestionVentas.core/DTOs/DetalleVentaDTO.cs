using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVentas.core.DTOs
{
    public class DetalleVentaDTO
    {
        //eso se añadió por ahora
        //public int ProductoId { get; set; } // Add this property to fix the error  
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
