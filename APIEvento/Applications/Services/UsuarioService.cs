using System.Security.Cryptography;
using System.Text;
using APIEvento.Domains;
using APIEvento.DTOs.UsuarioDTO;
using APIEvento.Interfaces;
using APIEvento.Exceptions;

namespace APIEvento.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        // injeção de dependencias
        // implementamos o repositório e o service só depende da interface
        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        private static LerUsuarioDTO LerDto(Usuario usuario) // pega a entidade usuario e gera um DTO
        {
            LerUsuarioDTO lerUsuario = new LerUsuarioDTO
            {
                UsuarioID = usuario.UsuarioId,
                Nome = usuario.Nome,
                Email = usuario.Email,
                TipoUsuario = usuario.TipoUsuarioID,
                Especialidade = usuario.EspecialidadeID 
            };
            return lerUsuario;
        }

        public List<LerUsuarioDTO> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();
            List<LerUsuarioDTO> usuariosDTO = usuarios
                .Select(u => LerDto(u)).ToList();
            return usuariosDTO;
        }

        private static void ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                throw new DomainException("Email inválido.");
            }
        }

        private static byte[] HashSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha)) // garante que a senha não está vazia
            {
                throw new DomainException("Senha é obrigatória.");
            }

            using var sha256 = SHA256.Create(); // gera um hash SHA256 e devolve em byte[]
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));
        }

        public LerUsuarioDTO ObterPorId(int id)
        {
            Usuario? usuario = _repository.ObterPorId(id);

            if (usuario == null)
            {
                throw new DomainException("Usuário não existe.");
            }

            return LerDto(usuario); // se existe usuário, converte para DTO e devolve o usuário.
        }

        public LerUsuarioDTO ObterPorEmail(string email)
        {
            Usuario? usuario = _repository.ObterPorEmail(email);

            if (usuario == null)
            {
                throw new DomainException("Usuário não existe.");
            }

            return LerDto(usuario); // se existe usuário, converte para DTO e devolve o usuário.
        }


        public LerUsuarioDTO Adicionar(CriarUsuarioDTO usuarioDto)
        {
            if (usuarioDto.TipoUsuarioID != 1)
            {
                throw new DomainException("Tipo de usuário inválido. Apenas tipo 1 (administrador) é permitido.");
            }

            ValidarEmail(usuarioDto.Email);

            if (_repository.EmailExiste(usuarioDto.Email))
            {
                throw new DomainException("Já existe um usuário com este e-mail");
            }

            if (usuarioDto.TipoUsuarioID <= 0)
            {
                throw new DomainException("Tipo de usuário não pode ser nulo.");
            }

            Usuario usuario = new Usuario // criando entidade usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = HashSenha(usuarioDto.Senha),
                TipoUsuarioID = usuarioDto.TipoUsuarioID,
                EspecialidadeID = usuarioDto.EspecialidadeID
            };


            _repository.Adicionar(usuario);

            return LerDto(usuario);

        }

        public LerUsuarioDTO Atualizar(int id, CriarUsuarioDTO usuarioDto)
        {

            if (usuarioDto.TipoUsuarioID != 1)
            {
                throw new DomainException("Tipo de usuário inválido. Apenas tipo 1 (administrador) é permitido.");
            }

            Usuario usuarioBanco = _repository.ObterPorId(id);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            ValidarEmail(usuarioDto.Email);

            Usuario usuarioComMesmoEmail = _repository.ObterPorEmail(usuarioDto.Email);

            if (usuarioComMesmoEmail != null && usuarioComMesmoEmail.UsuarioId != id)
            {
                throw new DomainException("Já existe um usuário com este e-mail.");
            }

            //Substitui as informações do banco (usuarioBanco)
            //Inserindo as alterações que estão vindo de usuarioDto.
            usuarioBanco.Nome = usuarioDto.Nome;
            usuarioBanco.Email = usuarioDto.Email;
            usuarioBanco.Senha = HashSenha(usuarioDto.Senha);
            usuarioBanco.TipoUsuarioID = usuarioDto.TipoUsuarioID;
            usuarioBanco.EspecialidadeID = usuarioDto.EspecialidadeID;

            _repository.Atualizar(usuarioBanco);

            return LerDto(usuarioBanco);
        }

        public void Remover(int id)
        {

           

            Usuario usuario = _repository.ObterPorId(id);

            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            _repository.Remover(id);
        }
    }
}
