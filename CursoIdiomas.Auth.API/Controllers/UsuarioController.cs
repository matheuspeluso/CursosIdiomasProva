using CursoIdiomas.Domain.Dtos;
using CursoIdiomas.Domain.Interfacies.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursoIdiomas.Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService? _usuarioService;

        public UsuarioController(IUsuarioService? usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("Criar")]
        public IActionResult CriarUsuario(CriarUsuarioRequest request)
        {
            try
            {
                return Ok(_usuarioService.CriarUsuario(request));
            }
            catch(ApplicationException e)
            {
                return BadRequest(new { e.Message });
            }
        }


        [HttpPost("Autenticar")]
        public IActionResult AutenticarUsuario(AutenticarUsuarioRequest request)
        {
            try
            {
                return Ok(_usuarioService.AutenticarUsuario(request));
            }catch(ApplicationException e)
            {
                return BadRequest(new { e.Message });
            }
        }
    }
}
