using APIEvento.Domains;

namespace APIEvento.Interfaces
{
    public interface IEventoRepository
    {
        List<Evento> Listar();
        Evento ObterPorId(int id);
        bool NomeExiste(string nome, int? EventoIdAtual = null);
        void Adicionar(Evento evento);
        void Atualizar(Evento evento);
        void Remover(int id);
    }
}
