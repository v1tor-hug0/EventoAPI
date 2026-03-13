namespace APIEvento.DTOs.UsuarioDTO
{
    public class LerUsuarioDTO
    {
        public int UsuarioID { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int TipoUsuario { get; set; }
        public int? Especialidade { get; set; }
    }
}
