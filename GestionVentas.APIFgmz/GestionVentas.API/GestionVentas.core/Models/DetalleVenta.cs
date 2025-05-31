using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVentas.core.Models
{
    public class DetalleVenta
    {
        public int Id { get; set; } //de la venta
        public int VentaId { get; set; } //de la venta
        public int ProductoId { get; set; } //del producto
        public Venta Venta { get; set; } //producto vendido
        public Producto Producto { get; set; } //producto vendido

        public int Cantidad { get; set; } //cantidad vendida
        public decimal PrecioUnitario { get; set; } //precio del producto
        public decimal Subtotal { get; set; } //total de la venta



    }
}
