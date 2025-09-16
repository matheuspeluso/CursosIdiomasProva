namespace CursoIdiomas.Domain.Entities
{
    public class Turma
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Disciplina { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Numero { get; set; } = "0";
        public string AnoLetivo { get; set; } = "0";
        public DateTime? DataExclusao { get; set; } = null;

        #region Relacionamento
        public List<AlunoTurma> AlunoTurmas { get; set; } = new();
        #endregion
    }
}
