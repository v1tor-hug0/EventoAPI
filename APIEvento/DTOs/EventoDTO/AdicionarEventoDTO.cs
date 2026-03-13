using APIEvento.Domains;

namespace APIEvento.DTOs.EventoDTO
{
    public class AdicionarEventoDTO
    {
        public string Nome { get; set; } = null!;
        public string Local { get; set; } = null!;
        public DateTime DataEvento { get; set; }

        // Lista de IDs de usuários que serão inscritos no evento
        public List<int> InscritosUsuarioIds { get; set; } = new();
    }
}
