using System;
using System.Collections.Generic;

namespace APIEvento.Domains;

public partial class Log_AlteracaoProduto
{
    public int Log_AlteracaoEventoID { get; set; }

    public DateTime DataAlteracao { get; set; }

    public string? NomeAnterior { get; set; }

    public decimal? DataAnterior { get; set; }

    public int? EventoID { get; set; }

    public virtual Evento? Evento { get; set; }
}
