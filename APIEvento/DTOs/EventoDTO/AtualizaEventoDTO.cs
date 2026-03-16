using APIEvento.Domains;

namespace APIEvento.DTOs.EventoDTO
{
    public class AtualizaEventoDTO
    {
        public string Nome { get; set; } = null!;
        public string Local { get; set; } = null!;
        public DateTime DataEvento { get; set; }
        public int UsuarioId { get; set; }
        public int TipoUsuarioId { get; set; }
        public int InscricaoId { get; set; }

        // Para atualizar as inscrições, forneça lista de IDs de usuário
        public List<int> InscritosUsuarioIds { get; set; } = new();
    }
}