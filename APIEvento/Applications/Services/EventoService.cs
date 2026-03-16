using System.Linq;
using APIEvento.Domains;
using APIEvento.DTOs.EventoDTO;
using APIEvento.Interfaces;
using APIEvento.Exceptions;
using APIEvento.Applications.Conversoes;

namespace APIEvento.Applications.Services
{
    public class EventoService
    {
        private readonly IEventoRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;

        public EventoService(IEventoRepository repository, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        public List<LerEventoDTO> Listar()
        {
            List<Evento> eventos = _repository.Listar();
            List<LerEventoDTO> eventosDTO = eventos
                .Select(EventoParaDTO.ConverterParaDto).ToList();
            return eventosDTO;
        }

        public LerEventoDTO ObterPorId(int id)
        {
            Evento? evento = _repository.ObterPorId(id);
            if (evento == null)
            {
                throw new DomainException("Evento não encontrado.");
            }
            return EventoParaDTO.ConverterParaDto(evento);
        }

        public LerEventoDTO Adicionar(AdicionarEventoDTO criarEventoDTO)
        {
            if (string.IsNullOrWhiteSpace(criarEventoDTO.Nome))
                throw new DomainException("Nome do evento é obrigatório.");

            if (string.IsNullOrWhiteSpace(criarEventoDTO.Local))
                throw new DomainException("Local do evento é obrigatório.");

            if (criarEventoDTO.DataEvento == default)
                throw new DomainException("Data do evento é obrigatória.");

            if (_repository.NomeExiste(criarEventoDTO.Nome))
                throw new DomainException("Já existe um evento com esse nome.");

            // Valida e mapeia os IDs de usuários para inscrições
            var inscricoes = criarEventoDTO.InscritosUsuarioIds?
                .Select(uId =>
                {
                    var usuario = _usuarioRepository.ObterPorId(uId);
                    if (usuario == null)
                        throw new DomainException($"Usuário com id {uId} não encontrado.");
                    return new Inscricao { UsuarioId = uId };
                })
                .ToList() ?? new List<Inscricao>();

            Evento evento = new Evento
            {
                Nome = criarEventoDTO.Nome,
                Local = criarEventoDTO.Local,
                DataEvento = criarEventoDTO.DataEvento,
                Inscricao = criarEventoDTO.InscricaoId
            };

            _repository.Adicionar(evento);

            return EventoParaDTO.ConverterParaDto(evento);
        }

        public LerEventoDTO Atualizar(AtualizaEventoDTO atualizarEventoDTO, int id)
        {
            Evento? eventoBanco = _repository.ObterPorId(id);
            if (eventoBanco == null)
            {
                throw new DomainException("Evento não encontrado.");
            }

            if (_repository.NomeExiste(atualizarEventoDTO.Nome, EventoIdAtual: id))
            {
                throw new DomainException("Já existe um evento com esse nome.");
            }

            eventoBanco.Nome = atualizarEventoDTO.Nome;
            eventoBanco.Local = atualizarEventoDTO.Local;
            eventoBanco.DataEvento = atualizarEventoDTO.DataEvento;
            eventoBanco.UsuarioId = atualizarEventoDTO.UsuarioId; 
            eventoBanco.TipoUsuarioID = atualizarEventoDTO.TipoUsuarioId;
            eventoBanco.InscricaoId = atualizarEventoDTO.InscricaoId;


            _repository.Atualizar(eventoBanco);
            return EventoParaDTO.ConverterParaDto(eventoBanco);
        }

        public void Remover(int id)
        {
            Evento? eventoBanco = _repository.ObterPorId(id);
            if (eventoBanco == null)
            {
                throw new DomainException("Evento não encontrado.");
            }
            _repository.Remover(id);
        }
    }
}