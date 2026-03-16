using System;
using System.Collections.Generic;

namespace APIEvento.Domains;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] Senha { get; set; } = null!;

    public int TipoUsuarioID { get; set; }

    public int? EspecialidadeID { get; set; }

    public virtual Especialidade? Especialidade { get; set; }

    public virtual ICollection<Evento> Evento { get; set; } = new List<Evento>();

    public virtual ICollection<Inscricao> Inscricao { get; set; } = new List<Inscricao>();

    public virtual TipoUsuario TipoUsuario { get; set; } = null!;
}
