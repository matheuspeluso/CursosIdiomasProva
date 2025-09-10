using CursoIdiomas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoIdiomas.Infra.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("TB_USUARIO");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID").IsRequired();
            builder.Property(x=> x.Nome).HasColumnName("NOME").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(x => x.Email).HasColumnName("EMAIL").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(x => x.Senha).HasColumnName("SENHA").HasColumnType("VARCHAR(100)").IsRequired();

            builder.HasIndex(x=> x.Email).IsUnique();
        }
    }
}
