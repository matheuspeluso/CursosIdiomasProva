using CursoIdiomas.Domain.Dtos;
using CursoIdiomas.Domain.Interfacies.Services;
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
        public TurmaResponse BuscarTurmaPorId(Guid id)
        {
            try
            {
                var response = _turmaService.BuscarTurmaPorId(id);
                return response;
            }
            catch (ApplicationException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("buscarTodasTurmas")]
        public List<TurmaResponse> BuscarTodasTurmas()
        {
            try
            {
                var response  = _turmaService.BuscarTodasTurmas();
                return response;
            }
            catch (ApplicationException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
