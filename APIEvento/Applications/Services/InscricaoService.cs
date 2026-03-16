using APIEvento.Domains;
using APIEvento.DTOs.InscricaoDTO;
using APIEvento.Exceptions;
using APIEvento.Interfaces;


namespace APIEvento.Applications.Services
{
    public class InscricaoService
    {
        private readonly IInscricaoRepository _repository;

        public InscricaoService(IInscricaoRepository repository)
        {
            _repository = repository;
        }

        private static LerInscricaoDTO LerDto(Inscricao inscricao)
        {
            return new LerInscricaoDTO
            {
                InscricaoID = inscricao.InscricaoId,
                UsuarioID = inscricao.UsuarioId,
                EventoID = inscricao.EventoId
            };
        }

        public List<LerInscricaoDTO> Listar()
        {
            List<Inscricao> inscricoes = _repository.Listar();

            List<LerInscricaoDTO> inscricaoDTOs =
                inscricoes.Select(inscricoesBanco => LerDto(inscricoesBanco)).ToList();
            return inscricaoDTOs;
        }

        public LerInscricaoDTO ObterPorId(int id)
        {
            Inscricao? inscricao = _repository.ObterPorId(id);

            if (inscricao == null)
            {
                throw new DomainException("Inscrição não encontrada.");
            }

            return LerDto(inscricao);
        }

        // Agora recebe o DTO do controller, valida e mapeia para entidade
        public LerInscricaoDTO Adicionar(AdicionarInscricaoDTO inscricaoDto)
        {
            if (inscricaoDto == null)
                throw new DomainException("Dados de inscrição inválidos.");

            // validações básicas
            if (inscricaoDto.UsuarioID <= 0)
                throw new DomainException("UsuarioID inválido.");
            if (inscricaoDto.EventoID <= 0)
                throw new DomainException("EventoID inválido.");

            // verifica se já existe inscrição (usa assinatura com usuarioId, eventoId)
            if (_repository.InscricaoExiste(inscricaoDto.UsuarioID, inscricaoDto.EventoID))
            {
                throw new DomainException("O usuário já está inscrito neste evento.");
            }

            Inscricao novaInscricao = new Inscricao
            {
                UsuarioId = inscricaoDto.UsuarioID,
                EventoId = inscricaoDto.EventoID
            };

            _repository.Adicionar(novaInscricao);

            return LerDto(novaInscricao);
        }

        public void Remover(int id)
        {
            Inscricao? inscricaoBanco = _repository.ObterPorId(id);
            if (inscricaoBanco == null)
            {
                throw new DomainException("Inscrição não encontrada.");
            }
            _repository.Remover(id);
        }
    }
}