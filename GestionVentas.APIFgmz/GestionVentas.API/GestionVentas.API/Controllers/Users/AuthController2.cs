using GestionVentas.core.DTOs.Users;
using GestionVentas.core.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace GestionVentas.API.Controllers.Users
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController2 : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController2(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            var result = await _authService.RegistrarUsuarioAsync(model);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var token = await _authService.LoginAsync(model);
            if (token == null) return Unauthorized();
            return Ok(new { Token = token });
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditUser(EditUserDto model)
        {
            var result = await _authService.EditarUsuarioAsync(model);
            if (!result) return BadRequest("Error al editar usuario");
            return Ok("Usuario editado correctamente");
        }

        [HttpDelete("delete/{userName}")]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            var result = await _authService.EliminarUsuarioAsync(userName);
            if (!result) return NotFound("Usuario no encontrado");
            return Ok("Usuario eliminado");
        }
    }

}
