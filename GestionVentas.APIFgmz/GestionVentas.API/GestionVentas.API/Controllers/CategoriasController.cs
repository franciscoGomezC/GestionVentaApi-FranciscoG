using GestionVentas.core.DTOs;
using GestionVentas.core.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionVentas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        //inyección de depencias de la clase CategoriaService
        private readonly CategoriaService _categoriaService;

        //constructor de la clase CategoriasController
        public CategoriasController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        // GET: CategoriasController para obtener todas las categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetAll()
        {
            var categorias = await _categoriaService.GetAllAsync(); //obtiene todas las categorias
            return Ok(categorias); //retorna las categorias en formato JSON
        }

        /// get por id
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDTO>> GetById(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id); //obtiene una categoria por su id
            if (categoria == null) return NotFound($"Categoria con id {id} no encontrada"); //si no existe la categoria retorna 404
            return Ok(categoria); //retorna la categoria en formato JSON
        }

        // POST para crear una categoria
        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> Create(CategoriaDTO categoriaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //valida el modelo
            var categoria = await _categoriaService.AddAsync(categoriaDto); //agrega la categoria
            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria); //retorna 201
        }

        // PUT para actualizar una categoria
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(CategoriaDTO categoriaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //valida el modelo
            try
            {
                await _categoriaService.UpdateAsync(categoriaDto); //actualiza la categoria
                return NoContent(); //retorna 204
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); //retorna 400
            }
        }

        // DELETE para eliminar una categoria
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _categoriaService.DeleteAsync(id); //elimina la categoria
                return NoContent(); //retorna 204
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); //si no existe la categoria retorna 404
            }
        }
    }
}
