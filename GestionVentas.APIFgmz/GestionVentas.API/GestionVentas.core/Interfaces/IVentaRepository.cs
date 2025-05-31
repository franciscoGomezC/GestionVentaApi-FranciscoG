using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionVentas.core.Models;

namespace GestionVentas.core.Interfaces
{
    public interface IVentaRepository
    {
        Task<IEnumerable<Venta>> GetAllAsync(); //Obtiene todas las ventas
        Task<Venta> GetByIdAsync(int id); //Obtiene una venta por su ID
        Task AddAsync(Venta venta); //Agrega una nueva venta
        Task UpdateAsync(Venta venta); //Actualiza una venta
        Task DeleteAsync(int id); //Elimina una venta por su ID
    }
}
