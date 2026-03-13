using APIEvento.DTOs;
using APIEvento.Domains;
using APIEvento.DTOs.EventoDTO;

namespace APIEvento.Applications.Conversoes
{
    public class EventoParaDTO
    {
        public static LerEventoDTO ConverterParaDto(Evento evento)
        {
            return new LerEventoDTO
            {
                EventoID = evento.EventoId,
                Nome = evento.Nome,
                Data = evento.DataEvento,
                Local = evento.Local
            };
        }
    }
}
