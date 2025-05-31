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
    public class ProductoRepository: IProductoRepository
    {
        private readonly ApplicationDbContext _context;

        //Constructor que recibe el contexto de la base de datos
        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Metodo para obtener todos los productos
        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            return await _context.Productos
                .Include(p => p.Categoria) //Incluye la categoria del producto
                .ToListAsync(); //Convierte a lista asincronamente
        }

        //Metodo para obtener un producto por su id
        public async Task<Producto> GetByIdAsync(int id)
        {
            return await _context.Productos
                .Include(p => p.Categoria) //Incluye la categoria del producto
                .FirstOrDefaultAsync(p => p.Id == id); //Obtiene el primer producto que coincida con el id
        }


        //Metodo para agregar un nuevo producto
        public async Task<Producto> GetByCodigoAsync(string codigo)
        {
            return await _context.Productos
                .Include(p => p.Categoria) //Incluye la categoria del producto
                .FirstOrDefaultAsync(p => p.Codigo == codigo); //Obtiene el primer producto que coincida con el codigo
        }

        //Metodo para agregar un nuevo producto
        public async Task AddAsync(Producto producto)
        {
            await _context.Productos.AddAsync(producto); //Agrega el producto al contexto
            await _context.SaveChangesAsync(); //Guarda los cambios en la base de datos
        }

        //Metodo para actualizar un producto
        public async Task UpdateAsync(Producto producto)
        {
            _context.Productos.Update(producto); //Actualiza el producto en el contexto
            await _context.SaveChangesAsync(); //Guarda los cambios en la base de datos
        }

        //Metodo para eliminar un producto por su id
        public async Task DeleteAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id); //Obtiene el producto por su id
            if (producto != null) //Si el producto existe
            {
                _context.Productos.Remove(producto); //Elimina el producto del contexto
                await _context.SaveChangesAsync(); //Guarda los cambios en la base de datos
            }
        }


    }
}
