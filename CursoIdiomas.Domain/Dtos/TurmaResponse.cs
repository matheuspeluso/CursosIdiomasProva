namespace CursoIdiomas.Domain.Dtos
{
    public class TurmaResponse
    {
        public Guid Id { get; set; }
        public string? Numero { get; set; }
        public string? AnoLetivo { get; set; }
        public DateTime? DataExclusao { get; set; }
        public List<AlunoResponse>? AlunoTurmas { get; set; }
    }
}
