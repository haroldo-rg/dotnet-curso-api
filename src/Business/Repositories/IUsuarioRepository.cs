using Curso.Api.Business.Entities;

namespace Curso.Api.Business.Repositories
{
    public interface IUsuarioRepository
    {
        void Adicinar(Usuario usuario);
        void Commit();
        bool ValidarCredenciais(string login, string senhaCriptografada);
        Usuario ObterPorLogin(string login);
    }
}
