using Curso.Api.Business.Entities;
using Curso.Api.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Curso.Api.Infrastructure.Data
{
    public class CursoDbContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Business.Entities.Curso> Curso { get; set; }

        public CursoDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new CursoMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
