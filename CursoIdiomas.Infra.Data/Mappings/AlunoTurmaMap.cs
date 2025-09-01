using CursoIdiomas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CursoIdiomas.Infra.Data.Mappings
{
    public class AlunoTurmaMap : IEntityTypeConfiguration<AlunoTurma>
    {
        public void Configure(EntityTypeBuilder<AlunoTurma> builder)
        {
            builder.ToTable("TB_ALUNO_TURMA");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID").IsRequired();
            builder.Property(x => x.AlunoId).HasColumnName("ALUNO_ID").IsRequired();
            builder.Property(x => x.TurmaId).HasColumnName("TURMA_ID").IsRequired();
            builder.Property(x=> x.DataMatricula).HasColumnName("DATA_MATRICULA").HasColumnType("DATE").IsRequired();

            //regra para garantir que aluno não se matricule 2x na mesma turma
            builder.HasIndex(x => new { x.AlunoId, x.TurmaId }).IsUnique();
        }

    }
}
