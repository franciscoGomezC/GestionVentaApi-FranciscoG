using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionVentas.core.Models;

namespace GestionVentas.core.Interfaces
{
    public interface ICategoriaRepository
    {

        Task<IEnumerable<Categoria>> GetAllAsync(); //Obtiene todas las categorias
        Task<Categoria> GetByIdAsync(int id); //Obtiene una categoria por su ID
        Task AddAsync(Categoria categoria); //Agrega una nueva categoria
        Task UpdateAsync(Categoria categoria); //Actualiza una categoria
        Task DeleteAsync(int id); //Elimina una categoria por su ID
    }
}
