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
    public class ClienteRepository : IClienteRepository
    {
        //la variable de solo lectura para el db context
        private readonly ApplicationDbContext _context;

        //el constructor
        public ClienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //función para obtener todos los clientes
        public async Task<IEnumerable<Cliente>> GetAllAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        //función para obtener por id
        public async Task<Cliente> GetByIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        //añadir el cliente al contexto
        public async Task AddAsync(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        //Actualizar el contexto
        public async Task UpdateAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        //eliminar 
        public async Task DeleteAsync(int id)
        {
            var cliente = await GetByIdAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente); 
                await _context.SaveChangesAsync();
            }
        }



    }
}
