using CursoIdiomas.Domain.Dtos;
using CursoIdiomas.Domain.Interfacies.Services;
using Microsoft.AspNetCore.Mvc;

namespace CursoIdiomas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpPost("cadastrarAluno")]
        public IActionResult Post(AlunoRequest request)
        {
            try
            {
                var response = _alunoService.CadastrarAluno(request);
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

        [HttpPut("atualizarAluno/{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] AlunoRequest request)
        {
            try
            {
                var response = _alunoService.AtualizarAluno(id, request);
                return Ok(response);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno do servidor" });
            }
        }

        [HttpDelete("excluirAluno/{id}")] 
        public IActionResult Delete(Guid id)
        {
            try
            {
                var response = _alunoService.ExcluirAluno(id);
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

        [HttpGet("buscarAluno/{id}")] 
        public IActionResult Get(Guid id)
        {
            try
            {
                var response = _alunoService.BuscarAlunoPorId(id);
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

        [HttpGet("buscarTodasAlunos")] 
        public IActionResult GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if(pageSize > 20)
                return StatusCode(400, "O tamanho da página não pode ser maior que 20.");
            try
            {
                var response = _alunoService.BuscarAlunos(pageNumber, pageSize);
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
    }
}
