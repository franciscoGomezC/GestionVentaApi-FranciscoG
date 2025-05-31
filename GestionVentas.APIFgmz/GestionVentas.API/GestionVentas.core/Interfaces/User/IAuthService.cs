using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionVentas.core.DTOs.Users;

namespace GestionVentas.core.Interfaces.User
{
    public interface IAuthService
    {
        Task<AuthResultDto> RegistrarUsuarioAsync(RegisterDto model);
        Task<bool> EditarUsuarioAsync(EditUserDto model);
        Task<bool> EliminarUsuarioAsync(string userName);
        Task<string> LoginAsync(LoginDto model);
    }

}
