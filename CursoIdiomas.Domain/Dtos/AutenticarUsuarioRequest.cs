using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoIdiomas.Domain.Dtos
{
    public class AutenticarUsuarioRequest
    {
        public string? Email { get; set; }
        public string? Senha { get; set; }
    }
}
