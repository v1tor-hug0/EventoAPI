using Microsoft.EntityFrameworkCore;
using APIEvento.Contexts;
using APIEvento.Domains;
using APIEvento.Interfaces;

namespace APIEvento.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly EventosDBContext _context;

        public EventoRepository(EventosDBContext context)
        {
            _context = context;
        }

        public List<Evento> Listar()
        {
            List<Evento> eventos = _context.Evento.ToList();
            return eventos;
        }

        public Evento ObterPorId(int id)
        {
            Evento? evento = _context.Evento
                //Procura no banco com o aux produtoDb onde o ID do produto for igual ao id passado por parâmetro no metodo ObeterPorId
                .FirstOrDefault(eventoDB => eventoDB.EventoId == id);
            return evento;
        }

        public bool NomeExiste(string nome, int? EventoIdAtual = null)
        {
            var eventoAtual = _context.Evento.AsQueryable(); // AsQueryable -> Monta a consulta passo a passo, nao executa a consulta no banco ainda

            if (EventoIdAtual.HasValue)
            {
                eventoAtual = eventoAtual.Where(p => p.EventoId != EventoIdAtual.Value);
            }
            return eventoAtual.Any(produto => produto.Nome == nome);
        }

        public void Adicionar(Evento evento)
        {
            _context.Evento.Add(evento);
            _context.SaveChanges();
        }

        public void Atualizar(Evento evento)
        {
            Evento? eventoBanco = _context.Evento.FirstOrDefault(eventoAux => eventoAux.EventoId == evento.EventoId);
            if (eventoBanco == null)
            {
                return;
            }
            eventoBanco.Nome = evento.Nome;
            eventoBanco.DataEvento = evento.DataEvento;
            eventoBanco.Local = evento.Local;
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Evento? eventoBanco = _context.Evento.FirstOrDefault(eventoAux => eventoAux.EventoId == id);
            if (eventoBanco == null)
            {
                return;
            }
            _context.Evento.Remove(eventoBanco);
            _context.SaveChanges();
        }
    }
}
