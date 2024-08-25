using Autenticacion.DTO;
using Autenticacion.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autenticacion.BLL.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO> registrar(UsuarioDTO usuario);

        Task<Usuario> Login(LoginDTO loginDTO);
    }
}
