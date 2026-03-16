using APIEvento.Domains;


namespace APIEvento.Interfaces
{
    public interface IInscricaoRepository
    {
        List<Inscricao> Listar();

        // Pode não existir -> torna-se anulável
        Inscricao? ObterPorId(int id);

        // Verifica existência por usuário e evento
        bool InscricaoExiste(int usuarioId, int eventoId);

        void Adicionar(Inscricao inscricao);
        void Remover(int id);
    }
}