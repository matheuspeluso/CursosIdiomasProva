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
    public class AlunoMap : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("TB_ALUNO");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID").IsRequired();
            builder.Property(x => x.Nome).HasColumnName("NOME").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(x => x.Cpf).HasColumnName("CPF").HasColumnType("VARCHAR(11)").IsRequired();
            builder.Property(x => x.Email).HasColumnName("EMAIL").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(x => x.DataCadastro).HasColumnName("DATA_CADASTRO").HasColumnType("DATE").IsRequired();
            builder.Property(x=> x.DataExclusao).HasColumnName("DATA_EXCLUSAO").HasColumnType("DATE");

            //builder.HasIndex(x => x.Cpf).IsUnique();
            //builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => new
            {
                x.Cpf,
                x.Email
            });

            builder.HasMany(x => x.AlunoTurmas).WithOne(x => x.Aluno).HasForeignKey(x => x.AlunoId);

            builder.Property(x => x.RowVersion)
           .IsRowVersion()
           .IsConcurrencyToken();
        }
    }
}
