using Autenticacion.BLL.Servicios.Contrato;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Autenticacion.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _servicio;

        public ProductoController(IProductoService servicio)
        {
            _servicio=servicio;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> Lista()
        {
            var rsp=await _servicio.listar();

            return Ok(rsp);
        }
    }
}