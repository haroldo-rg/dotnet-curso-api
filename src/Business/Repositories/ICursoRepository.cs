using Curso.Api.Business.Entities;
using System.Collections.Generic;

namespace Curso.Api.Business.Repositories
{
    public interface ICursoRepository
    {
        void Adicinar(Business.Entities.Curso curso);
        void Commit();
        IList<Business.Entities.Curso> ObterPorUsuario(int codigoUsuario);
    }
}
