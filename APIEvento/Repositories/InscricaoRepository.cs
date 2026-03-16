using APIEvento.Domains;
using APIEvento.Interfaces;
using APIEvento.Contexts;

namespace APIEvento.Repositories
{
    public class InscricaoRepository : IInscricaoRepository
    {
        private readonly EventosDBContext _context;
        public InscricaoRepository(EventosDBContext context)
        {
            _context = context;
        }

        public List<Inscricao> Listar()
        {
            List<Inscricao> inscricoes = _context.Inscricao.ToList();
            return inscricoes;
        }

        public Inscricao? ObterPorId(int id)
        {
            Inscricao? inscricao = _context.Inscricao
                .FirstOrDefault(inscricaoDB => inscricaoDB.InscricaoId == id);
            return inscricao;
        }

        public bool InscricaoExiste(int usuarioId, int eventoId)
        {
            return _context.Inscricao
               .Any(i => i.UsuarioId == usuarioId && i.EventoId == eventoId);
        }

        public void Adicionar(Inscricao inscricao)
        {
            if(InscricaoExiste(inscricao.InscricaoId, inscricao.EventoId))
            {
                return;
            }
            _context.Inscricao.Add(inscricao);
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Inscricao? inscricao = _context.Inscricao.FirstOrDefault(inscricaoAux => inscricaoAux.InscricaoId == id);
            if (inscricao == null)
            {
                return;
            }
            _context.Inscricao.Remove(inscricao);
            _context.SaveChanges();
        }
    }
}
