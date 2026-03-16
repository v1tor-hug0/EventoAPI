using System;
using System.Collections.Generic;

namespace APIEvento.Domains;

public partial class Inscricao
{
    public int InscricaoId { get; set; }

    public int EventoId { get; set; }

    public int UsuarioId { get; set; }

    public virtual ICollection<Evento> Evento { get; set; } = new List<Evento>();

    public virtual Evento EventoNavigation { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
