using GestionVentas.core.DTOs;
using GestionVentas.core.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionVentas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        //variable de lectura de IClienteService (del servicio)
        private readonly ClienteService _clienteService;
        //constructor de la clase ClientesController
        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetAll()
        {
            var clientes = await _clienteService.GetAllAsync(); //obtiene todos los clientes
            return Ok(clientes); //retorna los clientes en formato JSON
        }

        // GET: api/clientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetById(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id); //obtiene un cliente por su id
            if (cliente == null) return NotFound($"Cliente con id {id} no encontrado"); //si no existe el cliente retorna 404
            return Ok(cliente); //retorna el cliente en formato JSON
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> Create(ClienteDTO clienteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //valida el modelo
            var cliente = await _clienteService.AddAsync(clienteDto); //agrega el cliente
            return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente); //retorna 201
        }

        // PUT: api/clientes/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(ClienteDTO clienteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); //valida el modelo
            try
            {
                await _clienteService.UpdateAsync(clienteDto); //actualiza el cliente
                return NoContent(); //retorna 204
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); //retorna 400
            }
        }

        // DELETE: api/clientes/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _clienteService.DeleteAsync(id); //elimina el cliente
                return NoContent(); //retorna 204
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); //retorna 400
            }
        }

    }
}
