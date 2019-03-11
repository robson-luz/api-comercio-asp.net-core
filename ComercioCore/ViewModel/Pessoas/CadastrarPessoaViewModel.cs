using ComercioCore.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.ViewModel.Pessoas
{
    public class CadastrarPessoaViewModel
    {
        [Required(ErrorMessage = "Campo '{0}' é obrigatório.")]
        [MaxLength(100, ErrorMessage = "Campo '{0}' deve ter no máximo {1} caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo '{0}' é obrigatório.")]
        [MaxLength(14, ErrorMessage = "Campo '{0}' deve ter no máximo {1} caracteres.")]
        [RegularExpression(ExpressaoRegular.CPF, ErrorMessage = "Campo '{0}' não está no formato adequado.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Campo '{0}' é obrigatório.")]
        [MaxLength(30, ErrorMessage = "Campo '{0}' deve ter no máximo {1} caracteres.")]
        [RegularExpression(ExpressaoRegular.EMAIL, ErrorMessage = "Campo '{0}' não está no formato adequado.")]
        public string Email { get; set; }

        [MaxLength(14, ErrorMessage = "Campo '{0}' deve ter no máximo {1} caracteres.")]
        [RegularExpression(ExpressaoRegular.CELULAR, ErrorMessage = "Campo '{0}' não está no formato adequado.")]
        public string NumeroCelular { get; set; }

        public char TipoPessoa { get; set; }

        [Required(ErrorMessage = "Login não informado.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite uma senha.")]
        public string Senha { get; set; }

        public IFormFile Arquivo { get; set; }
    }
}
