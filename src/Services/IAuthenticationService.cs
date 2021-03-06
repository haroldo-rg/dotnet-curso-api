using Curso.Api.Business.Entities;

namespace Curso.Api.Services
{
    public interface IAuthenticationService
    {
        string GerarToken(Usuario usuario);
    }
}
