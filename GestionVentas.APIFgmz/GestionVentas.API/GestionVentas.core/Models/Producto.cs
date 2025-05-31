using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVentas.core.Models
{
    public class Producto
    {

        //un producto debería de tener un id, nombre, precio y cantidad
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public int CategoriaId { get; set; } //llave foranea de la categoria
        public Categoria Categoria { get; set; }


    }
}
