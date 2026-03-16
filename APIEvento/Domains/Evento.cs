using System;
using System.Collections.Generic;

namespace APIEvento.Domains;

public partial class Evento
{
    public int EventoId { get; set; }

    public string Nome { get; set; } = null!;

    public DateTime DataEvento { get; set; }

    public string Local { get; set; } = null!;

    public int? UsuarioId { get; set; }

    public int? InscricaoId { get; set; }

    public int? TipoUsuarioID { get; set; }

    public virtual Inscricao? Inscricao { get; set; }

    public virtual ICollection<Inscricao> InscricaoNavigation { get; set; } = new List<Inscricao>();

    public virtual ICollection<Log_AlteracaoProduto> Log_AlteracaoProduto { get; set; } = new List<Log_AlteracaoProduto>();

    public virtual TipoUsuario? TipoUsuario { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
