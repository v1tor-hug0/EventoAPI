namespace APIEvento.DTOs.UsuarioDTO
{
    public class CriarUsuarioDTO
    {
        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Senha { get; set; } = null!;
        public int TipoUsuarioID { get; set; } 
        public int? EspecialidadeID { get; set; }
    }
}
