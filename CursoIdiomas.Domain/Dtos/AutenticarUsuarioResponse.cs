using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoIdiomas.Domain.Dtos
{
    public class AutenticarUsuarioResponse
    {
        public string? Mensagem { get; set; }

        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }

        public DateTime? DataHoraAcesso { get; set; }
        public DateTime? DataHoraExpiracao { get; set; }
        public string? AccessToken { get; set; }
    }
}
