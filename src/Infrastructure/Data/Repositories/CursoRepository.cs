using Curso.Api.Business.Entities;
using Curso.Api.Business.Repositories;
using Curso.Api.Business.Tools;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Curso.Api.Infrastructure.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CursoDbContext _context;

        public CursoRepository(CursoDbContext contexto)
        {
            _context = contexto;
        }

        public void Adicinar(Business.Entities.Curso curso)
        {
            _context.Curso.Add(curso);
        }

        public void Commit()
        {
            _context.SaveChangesAsync();
        }

        public IList<Business.Entities.Curso> ObterPorUsuario(int codigoUsuario)
        {
            return _context.Curso.Where(c => c.CodigoUsuario == codigoUsuario).Include(i => i.Usuario).ToList();
        }
    }
}
