using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVentas.core.Config
{
    // Interfaz para definir las opciones del JWT
    public interface IJWToptions
    {
        string Issuer { get; } // Emisor del token
        string Audience { get; } // Audiencia del token
        string SecretKey { get; } // Clave secreta para firmar el token
        int Duration { get; } // Duración de validez del token
    }

    public class JWTOptionsFactory : IJWToptions
    {
        public string Issuer { get; private set; }
        public string Audience { get; private set; }
        public string SecretKey { get; private set; }
        public int Duration { get; private set; }

        //Constructor para inicializar las opciones del JWT
        public JWTOptionsFactory(string issuer, string audience, string secretKey, int duration)
        {
            Issuer = issuer;
            Audience = audience;
            SecretKey = secretKey;
            Duration = duration;
        }
    }
}
