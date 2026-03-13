using System;
using System.Collections.Generic;

namespace APIEvento.Domains;

public partial class Inscricao
{
    public int InscricaoId { get; set; }

    public int EventoId { get; set; }

    public int UsuarioId { get; set; }

    public virtual Evento Evento { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
