namespace CursoIdiomas.Domain.Entities
{
    public class AlunoTurma
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AlunoId { get; set; }
        public Guid TurmaId { get; set; }

        public Aluno Aluno { get; set; }
        public Turma Turma { get; set; }
        public DateTime DataMatricula { get; set; } = DateTime.Now;
        public DateTime? DataExclusao { get; set; } = null;
    }
}
