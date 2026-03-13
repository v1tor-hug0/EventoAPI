using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using APIEvento.Applications.Services;
using APIEvento.DTOs.EventoDTO;
using APIEvento.Exceptions;


namespace APIEvento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly EventoService _service;

        public EventoController(EventoService service)
        {
            _service = service;
        }

        private int ObterUsuarioLogado()
        {
            //Busca no token/claims o valor armazenado como id do usuario
            //ClaimTypes.NameIdentifier geralmeente guarda o id do usuario no JWT
            string? idTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(idTexto))
            {
                throw new Exception("Usuário não autenticado.");
            }

            return int.Parse(idTexto);
        }

        [HttpGet]
        public ActionResult<List<LerEventoDTO>> Listar()
        {
            List<LerEventoDTO> evento = _service.Listar();

            //return StatusCode(200, evnto);
            return Ok(evento);
        }

        [HttpGet("{id}")]
        public ActionResult<LerEventoDTO> ObterPorId(int id)
        {
            LerEventoDTO evento = _service.ObterPorId(id);

            if (evento == null)
            {
                //return StatusCode(404);
                return NotFound();
            }

            return Ok(evento);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize] 
        public ActionResult Adicionar([FromForm] AdicionarEventoDTO eventoDto)
        {
            try
            {
                int UsuarioId = ObterUsuarioLogado();

                _service.Adicionar(eventoDto);
                return StatusCode(201);

            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
