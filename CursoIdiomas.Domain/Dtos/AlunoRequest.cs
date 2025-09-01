using CursoIdiomas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoIdiomas.Domain.Dtos
{
    public class AlunoRequest
    {
        [MaxLength(150, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres")]
        [MinLength(5, ErrorMessage = "O campo {0} deve ter no mínimo {1} caracteres")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter exatamente 11 números")]
        public string? Cpf { get; set; }

        [EmailAddress(ErrorMessage = "O campo {0} é inválido")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public List<Guid> TurmasIds { get; set; } = new();
    }
}
