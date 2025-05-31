using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GestionVentas.core.Models.Login
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? UserType { get; set; }
    }
}
