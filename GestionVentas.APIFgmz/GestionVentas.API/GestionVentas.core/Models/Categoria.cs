using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVentas.core.Models
{
    public class Categoria
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        //metodos parecidos al list 
        //categoria que solamente es una lista 
        //collection de productos es como un array, una lista
        public ICollection<Producto> Productos { get; set; }



    }
}
