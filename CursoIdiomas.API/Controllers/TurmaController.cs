using CursoIdiomas.Domain.Dtos;
using CursoIdiomas.Domain.Interfacies.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CursoIdiomas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaService _turmaService;

        public TurmaController(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        [HttpPost("cadastrarTurma")]
        [Authorize]
        public IActionResult Post([FromBody]TurmaRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = _turmaService.CadastrarTurma(request);
                return Ok(response);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("atualizarTurma/{id}")]
        [Authorize]
        public IActionResult Put(Guid id, [FromBody] TurmaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = _turmaService.AtualizarTurma(id, request);
                return Ok(response);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("excluirTurma/{id}")]
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var response = _turmaService.ExcluirTurma(id);
                return Ok(response);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("buscarTurmaPorId/{id}")]
        [Authorize]
        public IActionResult BuscarTurmaPorId(Guid id)
        {
            try
            {
                var response = _turmaService.BuscarTurmaPorId(id);
                return Ok(response);
            }
            catch (ApplicationException ex)
            {
                return  StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("buscarTodasTurmas")]
        [Authorize]
        public IActionResult BuscarTodasTurmas([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if(pageSize > 20)
                return StatusCode(400, "O tamanho da página não pode ser maior que 20.");
            try
            {
                var response  = _turmaService.BuscarTodasTurmas(pageNumber, pageSize);
                return Ok(response);
            }
            catch (ApplicationException ex)
            {
               return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
               return StatusCode(500, ex.Message);
            }
        }
    }
}
