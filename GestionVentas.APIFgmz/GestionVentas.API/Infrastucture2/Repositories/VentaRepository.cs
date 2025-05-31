using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionVentas.core.Interfaces;
using GestionVentas.core.Models;
using GestionVentas.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionVentas.Infrastructure.Repositories
{
    public class VentaRepository : IVentaRepository
    {
        //se trae el contexto de la base de datos
        private readonly ApplicationDbContext _context;

        //Constructor que recibe el contexto de la base de datos
        public VentaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Metodo para obtener todas las ventas
        public async Task<IEnumerable<Venta>> GetAllAsync()
        {
            return await _context.Ventas
                .Include(v => v.Cliente) //Incluye el cliente de la venta
                .Include(v => v.DetallesVenta) //Incluye los detalles de la venta
                    .ThenInclude(d => d.Producto) //Incluye el producto de los detalles de la venta
                .ToListAsync(); //Convierte a lista asincronamente
        }

        //Metodo para obtener una venta por su id
        public async Task<Venta> GetByIdAsync(int id)
        {
            return await _context.Ventas
                .Include(v => v.Cliente) //Incluye el cliente de la venta
                .Include(v => v.DetallesVenta) //Incluye los detalles de la venta
                    .ThenInclude(d => d.Producto) //Incluye el producto de los detalles de la venta
                .FirstOrDefaultAsync(v => v.Id == id); //Obtiene la primera venta que coincida con el id
        }

        //Metodo para obtener una venta por su id de cliente
        public async Task<IEnumerable<Venta>> GetByClienteIdAsync(int clienteId)
        {
            return await _context.Ventas
                .Include(v => v.Cliente) //Incluye el cliente de la venta
                .Include(v => v.DetallesVenta) //Incluye los detalles de la venta
                    .ThenInclude(d => d.Producto) //Incluye el producto de los detalles de la venta
                .Where(v => v.ClienteId == clienteId) //Filtra las ventas por el id del cliente
                .ToListAsync(); //Convierte a lista asincronamente
        }

        //Metodo para obtener una venta por su fecha
        public async Task<IEnumerable<Venta>> GetByFechaAsync(DateTime fecha)
        {
            return await _context.Ventas
                .Include(v => v.Cliente) //Incluye el cliente de la venta
                .Include(v => v.DetallesVenta) //Incluye los detalles de la venta
                    .ThenInclude(d => d.Producto) //Incluye el producto de los detalles de la venta
                .Where(v => v.Fecha.Date == fecha.Date) //Filtra las ventas por la fecha
                .ToListAsync(); //Convierte a lista asincronamente
        }

        //Metodo para agregar una nueva venta
        public async Task AddAsync(Venta venta)
        {
            await _context.Ventas.AddAsync(venta); //Agrega la venta al contexto
            await _context.SaveChangesAsync(); //Guarda los cambios en la base de datos
        }

        //Metodo para actualizar una venta
        public async Task UpdateAsync(Venta venta)
        {
            _context.Ventas.Update(venta); //Actualiza la venta en el contexto
            await _context.SaveChangesAsync(); //Guarda los cambios en la base de datos
        }

        //Metodo para eliminar una venta por su id
        public async Task DeleteAsync(int id)
        {
            var venta = await _context.Ventas.FindAsync(id); //Obtiene la venta por su id
            if (venta != null) //Si la venta existe
            {
                _context.Ventas.Remove(venta); //Elimina la venta del contexto
                await _context.SaveChangesAsync(); //Guarda los cambios en la base de datos
            }
        }

    }
}
