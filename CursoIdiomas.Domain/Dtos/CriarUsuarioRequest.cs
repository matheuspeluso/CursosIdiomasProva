using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoIdiomas.Domain.Dtos
{
    public class CriarUsuarioRequest
    {
        [MaxLength(150, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [MinLength(6, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o campo {0}.")]
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "Informe um e-mail valido!")]
        [Required(ErrorMessage = "Informe o campo {0}.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}.")]
        [RegularExpression(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}", ErrorMessage = "A senha deve conter no mínimo 8 caracteres, uma letra maiúscula, uma letra minúscula e um número")]
        public string? Senha { get; set; }
    }
}
