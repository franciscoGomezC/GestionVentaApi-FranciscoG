using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionVentas.core.Models;

namespace GestionVentas.core.Interfaces
{
    public interface IProductoRepository
    {

        Task<IEnumerable<Producto>> GetAllAsync(); //Obtiene todos los productos
        Task<Producto> GetByIdAsync(int id); //Obtiene un producto por su ID
        Task AddAsync(Producto producto); //Agrega un nuevo producto
        Task UpdateAsync(Producto producto); //Actualiza un producto
        Task DeleteAsync(int id); //Elimina un producto por su ID
    }
}
