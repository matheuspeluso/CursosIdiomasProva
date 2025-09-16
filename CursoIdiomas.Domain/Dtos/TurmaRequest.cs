using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoIdiomas.Domain.Dtos
{
    public class TurmaRequest
    {
        [MaxLength(8, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        [MinLength(3, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Numero { get; set; }

        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        [MinLength(3, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Disciplina { get; set; }

        [MaxLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        public string? Descricao { get; set; }

        [MaxLength(4, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        [MinLength(4, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? AnoLetivo { get; set; }
    }
}
