using CursoIdiomas.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoIdiomas.Infra.Data.Contexts
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=master;User ID=sa;Password=Prova2025;Encrypt=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfiguration(new AlunoMap());
           modelBuilder.ApplyConfiguration(new TurmaMap());
           modelBuilder.ApplyConfiguration(new AlunoTurmaMap());
        }
    }
}
