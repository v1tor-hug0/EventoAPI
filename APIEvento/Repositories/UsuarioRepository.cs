
using APIEvento.Contexts;
using APIEvento.Domains;
using APIEvento.Interfaces;
namespace APIEvento.Repositories

{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly EventosDBContext _context;

        public UsuarioRepository(EventosDBContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.ToList();
        }

        public Usuario? ObterPorId(int id)
        {
            return _context.Usuario.Find(id);
        }

        public Usuario? ObterPorEmail(string email)
        {
            return _context.Usuario.FirstOrDefault(Usuario => Usuario.Email == email);
        }

        public bool EmailExiste(string email)
        {
            return _context.Usuario.Any(Usuario => Usuario.Email == email);
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public void Atualizar(Usuario usuario)
        {
            Usuario? usuarioBanco = _context.Usuario.FirstOrDefault(usuarioAux => usuarioAux.UsuarioId == usuario.UsuarioId);
            if (usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.Email = usuario.Email;
            usuarioBanco.Senha = usuario.Senha;
            usuarioBanco.TipoUsuarioID = usuario.TipoUsuarioID;
            usuarioBanco.EspecialidadeID = usuario.EspecialidadeID;
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Usuario? usuario = _context.Usuario.FirstOrDefault(usuarioAux => usuarioAux.UsuarioId == id);
            if (usuario == null)
            {
                return;
            }
            _context.Usuario.Remove(usuario);
            _context.SaveChanges();

        }
    }
}
