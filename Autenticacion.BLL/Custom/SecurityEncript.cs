using Autenticacion.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.BLL.Custom
{
    public class SecurityEncript
    {
        private readonly IConfiguration _configuration;

        public SecurityEncript(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        public string encriptarSHA256(string texto)
        {
            using(SHA256 sha256Hash=SHA256.Create())
            {
               //computar el hash

               byte[] bytes=sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(texto));

               //convertir de bytes a string
               StringBuilder builder = new StringBuilder();

               for(int i=0;i<bytes.Length;i++)
               {
                   builder.Append(bytes[i].ToString("x2"));
               }
               return builder.ToString();
            }
        }

        public string generarJWT(Usuario modelo)
        {
            //crea la información del usuario para el TOken
            var userClaims = new[]
            {
                //puedes agregar mas atributos si deseas sobre el usuario para que generes el Token
                new Claim(ClaimTypes.NameIdentifier, modelo.Idusuario.ToString()),
                new Claim(ClaimTypes.Email, modelo.Correo!)
            };

            var SecurityKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]!));

            var credenciales = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //crear el detalle del token
            var jwtConfig = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credenciales
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtConfig);

           
        }
    }
}
