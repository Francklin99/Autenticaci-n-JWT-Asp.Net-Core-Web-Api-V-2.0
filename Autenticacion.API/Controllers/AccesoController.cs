using Autenticacion.DTO;
using Autenticacion.API.Utilidad;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Autenticacion.BLL.Servicios.Contrato;
using Autenticacion.BLL.Custom;

namespace Autenticacion.API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly IUsuarioService _servicio;
        private readonly SecurityEncript _encript;

        public AccesoController(IUsuarioService servicio, SecurityEncript encript)
        {
            _servicio = servicio;
            _encript = encript;
        }
        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Registrarse([FromBody]UsuarioDTO objeto)
        {
            var rsp = new Response<UsuarioDTO>();
            try
            {
                rsp.status = true;
                rsp.Value=await _servicio.registrar(objeto);
                rsp.message="Registro Exitoso";
            }
            catch(Exception ex)
            {
                rsp.status = false;
                rsp.message=ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginDTO objeto)
        {
            var usuarioEncontrado=await _servicio.Login(objeto);
            

            if (usuarioEncontrado==null)
            {
                return StatusCode(StatusCodes.Status200OK,new {isSuccess=false,token=""});
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _encript.generarJWT(usuarioEncontrado)});
            }
            
           
        }

    }
}
