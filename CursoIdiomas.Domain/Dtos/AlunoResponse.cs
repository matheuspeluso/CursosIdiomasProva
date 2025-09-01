using CursoIdiomas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoIdiomas.Domain.Dtos
{
    public class AlunoResponse
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Cpf { get; set; }
        public string? Email { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<AlunoTurmaResponse> AlunoTurmas { get; set; } = new();
    }
}
