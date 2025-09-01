namespace CursoIdiomas.Domain.Entities
{
    public class Aluno
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public List<AlunoTurma> AlunoTurmas { get; set; } = new();
        public DateTime? DataExclusao { get; set; } = null;
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }
}
