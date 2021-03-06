using Curso.Api.Business.Entities;
using Curso.Api.Business.Repositories;
using Curso.Api.Business.Tools;
using System.Linq;

namespace Curso.Api.Infrastructure.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CursoDbContext _context;
        private readonly ICriptografia _criptografia;

        public UsuarioRepository(CursoDbContext contexto, ICriptografia criptografia)
        {
            _context = contexto;
            _criptografia = criptografia;
        }

        public void Adicinar(Usuario usuario)
        {
            usuario.Senha = _criptografia.Encrypt(usuario.Senha);

            _context.Usuario.Add(usuario);
        }

        public void Commit()
        {
            _context.SaveChangesAsync();
        }

        public Usuario ObterPorLogin(string login)
        {
            return _context.Usuario.FirstOrDefault(u => u.Login == login);
        }

        public bool ValidarCredenciais(string login, string senha)
        {
            var usuario = _context.Usuario.FirstOrDefault(
                u => u.Login == login && u.Senha == _criptografia.Encrypt(senha)
            );

            return usuario != null;
        }
    }
}
