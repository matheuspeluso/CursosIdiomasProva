using CursoIdiomas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CursoIdiomas.Infra.Data.Mappings
{
    public class TurmaMap : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("TB_TURMA");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("ID").IsRequired();
            builder.Property(x => x.Numero).HasColumnName("NUMERO").HasColumnType("VARCHAR(10)").IsRequired();
            builder.Property(x => x.AnoLetivo).HasColumnName("ANO_LETIVO").HasColumnType("VARCHAR(10)").IsRequired();
            builder.Property(x=> x.DataExclusao).HasColumnName("DATA_EXCLUSAO").HasColumnType("DATE");
            builder.Property(x=> x.Disciplina).HasColumnName("DISCIPLINA").HasColumnType("VARCHAR(100)").IsRequired();
            builder.Property(x=> x.Descricao).HasColumnName("DESCRICAO").HasColumnType("VARCHAR(100)");

            builder.HasIndex(x => x.Numero).IsUnique();

            builder.HasMany(x => x.AlunoTurmas).WithOne(x => x.Turma).HasForeignKey(x => x.TurmaId);
        }
    }
}