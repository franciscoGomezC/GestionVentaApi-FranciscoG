using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVentas.core.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int ClienteId { get; set; } //id del cliente
        public int EmpleadoId { get; set; } //id del empleado
        public string MetodoPago { get; set; }
        public Cliente Cliente { get; set; }
        public Empleado Empleado { get; set; } //empleado que vendió
        public ICollection<DetalleVenta> DetallesVenta { get; set; } //productos vendidos


    }
}
