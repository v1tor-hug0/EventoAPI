using APIEvento.Domains;
using Microsoft.Identity.Client;

namespace APIEvento.DTOs.EventoDTO
{
    public class LerEventoDTO
    {
        public int EventoID { get; set; }
        public string Nome { get; set; } = null!;
        public string Local { get; set; } = null!;
        public DateTime Data { get; set; }
        public ICollection<Inscricao> Inscricao { get; set; }

        public int? UsuarioID { get; set; }
        public string? UsuarioNome { get; set; }
        public string? UsuarioEmail { get; set; }
    }
}
