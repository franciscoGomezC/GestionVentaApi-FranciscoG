using GestionVentas.core.DTOs;
using GestionVentas.core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionVentas.API.Controllers
{ 

    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {

        private readonly ProductoService _productoService; //Inyeccion de dependencias de la clase ProductoService

        //constructor de la clase ProductosController
        public ProductosController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        // GET: ProductosController/Create
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetAll()
        {
            var productos = await _productoService.GetAllAsync(); //obtiene todos los productos
            return Ok(productos); //retorna los productos en formato JSON
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> GetById(int id)
        {
            var producto = await _productoService.GetByIdAsync(id); //obtiene un producto por su id
            if (producto == null) return NotFound($"Prducto con id {id} no encontrado"); //si no existe el producto retorna 404
            
            return Ok(producto); //retorna el producto en formato JSON
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> Create(ProductoCreateDTO productoDto)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState); //valida el modelo

            var producto = await _productoService.AddAsync(productoDto); //agrega el producto

            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto); //retorna 201
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(ProductoDTO productoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //valida el modelo

            try
            {
                await _productoService.UpdateAsync(productoDto); //actualiza el producto
                return NoContent(); //retorna 204
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); //si no existe el producto retorna 404
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _productoService.DeleteAsync(id); //elimina el producto
                return NoContent(); //retorna 204
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); //si no existe el producto retorna 404
            }
        }
        //##############################################################################
        [ApiController]
        [Route("api/[controller]")]
        public class VentasController: ControllerBase
        {   
            //ventaservice de solo lectura
            private readonly VentaService _ventaService;

            //constructor
            public VentasController(VentaService ventaService)
            {
                _ventaService = ventaService;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<VentaDTO>>> GetAll()
            {
                var ventas = await _ventaService.GetAllAsync();
                return Ok(ventas);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<VentaDTO>> GetById(int id)
            {
                var venta = await _ventaService.GetByIdAsync(id);
                if (venta == null) return NotFound($"Venta con id {id} no encontrada");
                return Ok(venta);
            }

            //http post para crear una nueva venta
            [HttpPost]
            public async Task<ActionResult<VentaDTO>> Create(VentaCreateDTO ventaDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var venta = await _ventaService.AddAsync(ventaDto);
                return CreatedAtAction(nameof(GetById), new { id = venta.Id }, venta);
            }

            // http put para actualizar una venta
            [HttpPut("{id}")]
            public async Task<ActionResult> Update(int id, [FromBody] VentaDTO ventaDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id != ventaDto.Id)
                    return BadRequest("El ID de la URL no coincide con el del cuerpo de la solicitud.");

                try
                {
                    await _ventaService.UpdateAsync(ventaDto);
                    return NoContent();
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult> Delete(int id)
            {
                try
                {
                    await _ventaService.DeleteAsync(id);
                    return NoContent();
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
            }

        }

    }
}
