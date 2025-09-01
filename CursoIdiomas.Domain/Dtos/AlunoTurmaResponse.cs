using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoIdiomas.Domain.Dtos
{
    public class AlunoTurmaResponse
    {
        public Guid TurmaId { get; set; }
        public DateTime DataMatricula { get; set; }
    }
}
