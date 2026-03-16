using APIEvento.Applications.Services;
using APIEvento.DTOs.InscricaoDTO;
using APIEvento.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace APIEvento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscricaoController : ControllerBase
    {
        private readonly InscricaoService _service;

        public InscricaoController(InscricaoService service)
        {
            _service = service;
        }

        private int ObterUsuarioId()
        {
            string? idTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(idTexto))
            {
                throw new Exception("Usuário não autenticado.");
            }

            return int.Parse(idTexto);
        }

        [HttpGet]
        public ActionResult<List<LerInscricaoDTO>> Listar()
        {
            try
            {
                return Ok(_service.Listar());
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<LerInscricaoDTO> ObterPorId(int id)
        {
            LerInscricaoDTO produto = _service.ObterPorId(id);

            if (produto == null)
            {
                //return StatusCode(404);
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPost]
        //Indica que recebe dados no formato multpart/form-data, necessário para upload de arquivos
        [Consumes("multipart/form-data")]
        [Authorize] //Exige login para alterar os produtos

        // [FromForm] -> diz que os dados vem do formulario da requisição ("multipart/form-data")
        public ActionResult Adicionar([FromForm] AdicionarInscricaoDTO inscricaoDto)
        {
            try
            {
                int usuarioId = ObterUsuarioId();

                
                _service.Adicionar(inscricaoDto);
                return StatusCode(201);

            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize] 
        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}
