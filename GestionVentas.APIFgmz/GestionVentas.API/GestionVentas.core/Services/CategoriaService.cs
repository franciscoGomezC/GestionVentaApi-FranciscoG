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
    public class CategoriaService
    {
        //Inyeccion de dependencias de la interfaz ICategoriaRepository
        private readonly ICategoriaRepository _categoriaRepository;

        //constructor de la clase CategoriaService
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }


        //Metodo para obtener todas las categorias con CategoriaDTO
        public async Task<IEnumerable<CategoriaDTO>> GetAllAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync(); //obtiene todas las categorias
            return categorias.Select(c => new CategoriaDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
            });
        }

        //Metodo para obtener una categoria por su id
        public async Task<CategoriaDTO> GetByIdAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id); //obtiene una categoria por su id
            if (categoria == null) return null; //si no existe la categoria retorna null
            return new CategoriaDTO
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
            };
        }

        //Metodo para agregar una nueva categoria
        public async Task<CategoriaDTO> AddAsync(CategoriaDTO categoriaDTO)
        {
            var categoria = new Categoria
            {
                Nombre = categoriaDTO.Nombre,
                Descripcion = categoriaDTO.Descripcion,
            };
            await _categoriaRepository.AddAsync(categoria); //agrega la nueva categoria
            //retorna la categoria agregada
            return new CategoriaDTO
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
            };
        }


        //Metodo para actualizar una categoria
        public async Task<CategoriaDTO> UpdateAsync(CategoriaDTO categoriaDTO)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(categoriaDTO.Id); //obtiene la categoria por su id
            if (categoria == null) return null; //si no existe la categoria retorna null

            categoria.Nombre = categoriaDTO.Nombre; //actualiza el nombre de la categoria
            categoria.Descripcion = categoriaDTO.Descripcion; //actualiza la descripcion de la categoria
            await _categoriaRepository.UpdateAsync(categoria); //actualiza la categoria
            return new CategoriaDTO
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
            };
        }

        //Metodo para eliminar una categoria
        public async Task<bool> DeleteAsync(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id); //obtiene la categoria por su id
            if (categoria == null) return false; //si no existe la categoria retorna false
            await _categoriaRepository.DeleteAsync(id); //elimina la categoria
            return true; //retorna true si se elimino la categoria
        }

    }
}
