using CursoIdiomas.Domain.Entities;
using CursoIdiomas.Infra.Data.Mappings;
using CursoIdiomas.Infra.Data.Migrations;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace CursoIdiomas.Infra.Data.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=master;Persist Security Info=True;User ID=sa;Password=Prova2025;Encrypt=False"); //CONEXÃO PARA RODAR LOCAL
            optionsBuilder.UseSqlServer("Server=sqlserver-db,1433;Database=master;User Id=sa;Password=Prova2025;TrustServerCertificate=True;"); //CONEXÃO PARA RODAR NO DOCKER


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfiguration(new AlunoMap());
           modelBuilder.ApplyConfiguration(new TurmaMap());
           modelBuilder.ApplyConfiguration(new AlunoTurmaMap());
           modelBuilder.ApplyConfiguration(new UsuarioMap());
        }
    }
}
