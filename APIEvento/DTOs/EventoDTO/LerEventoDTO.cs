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


    }
}
