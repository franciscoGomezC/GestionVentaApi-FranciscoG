using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionVentas.core.DTOs;
using GestionVentas.core.Interfaces;
using GestionVentas.core.Models;

namespace GestionVentas.core.Services
{
    public class ProductoService
    {
        //en esta clase se implementa la logica de negocio para los productos
        //se inyecta el repositorio de productos

        private readonly IProductoRepository _productoRepository;
        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        //hacer la clase para obtener todos 
        public async Task<IEnumerable<ProductoDTO>> GetAllAsync()
        {
            var productos = await _productoRepository.GetAllAsync(); //obtiene todos los productos

            return productos.Select(p => new ProductoDTO
            {
                Id = p.Id,
                Codigo = p.Codigo,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Stock = p.Stock,
                Descripcion = p.Descripcion,
                CategoriaId = p.CategoriaId,
                CategoriaNombre = p.Categoria.Nombre,
            });
        }

        //hacer la clase para obtener un producto por id
        public async Task<ProductoDTO> GetByIdAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id); //obtiene un producto por su id
            if (producto == null) return null; //si no existe el producto retorna null
            return new ProductoDTO
            {
                Id = producto.Id,
                Codigo = producto.Codigo,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Stock = producto.Stock,
                Descripcion = producto.Descripcion,
                CategoriaNombre = producto.Categoria.Nombre,
            };
        }

        public async Task<ProductoDTO> AddAsync(ProductoCreateDTO productoDTO)
        {
            var producto = new Producto
            {
                Codigo = productoDTO.Codigo,
                Nombre = productoDTO.Nombre,
                Precio = productoDTO.Precio,
                Stock = productoDTO.Stock,
                Descripcion = productoDTO.Descripcion,
                CategoriaId = productoDTO.CategoriaId

            };

            await _productoRepository.AddAsync(producto); //agrega un nuevo producto
            //retorna el producto creado
            return new ProductoDTO
            {
                Id = producto.Id,
                Codigo = producto.Codigo,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Stock = producto.Stock,
                Descripcion = producto.Descripcion,
                CategoriaId = producto.CategoriaId
                //CategoriaNombre = producto.Categoria.Nombre,
            };
        }

        //hacer la clase para actualizar un producto
        public async Task UpdateAsync(ProductoDTO productoDTO)
        {
            var producto = await _productoRepository.GetByIdAsync(productoDTO.Id); //obtiene un producto por su id
            if (producto == null) return; //si no existe el producto retorna

            producto.Codigo = productoDTO.Codigo;
            producto.Nombre = productoDTO.Nombre;
            producto.Precio = productoDTO.Precio;
            producto.Stock = productoDTO.Stock;
            producto.Descripcion = productoDTO.Descripcion;
            producto.CategoriaId = productoDTO.CategoriaId;
            await _productoRepository.UpdateAsync(producto); //actualiza el producto
        }

        //hacer la clase para eliminar un producto
        public async Task DeleteAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id); //obtiene un producto por su id
            if (producto == null) return; //si no existe el producto retorna
            await _productoRepository.DeleteAsync(id); //elimina el producto
        }



    }
}
