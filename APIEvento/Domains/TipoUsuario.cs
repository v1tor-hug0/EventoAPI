using System;
using System.Collections.Generic;

namespace APIEvento.Domains;

public partial class TipoUsuario
{
    public int TipoUsuarioID { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Evento> Evento { get; set; } = new List<Evento>();

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
