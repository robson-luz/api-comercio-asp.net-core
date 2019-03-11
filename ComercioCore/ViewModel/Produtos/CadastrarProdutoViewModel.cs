using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.ViewModel.Produtos
{
    public class CadastrarProdutoViewModel
    {
        [Range(1, Int32.MaxValue, ErrorMessage = "Campo '{0}' é obrigatório.")]
        public int IdCategoria { get; set; }

        [Required(ErrorMessage ="Campo '{0}' é obrigatório.")]
        [MaxLength(50, ErrorMessage = "Campo '{0}' deve ter no máximo {1} caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Campo '{0}' é obrigatório.")]
        [Range(1, Int32.MaxValue, ErrorMessage = "Campo '{0}' só permite valores positivos.")]
        public decimal Preco { get; set; }

        public IFormFile Arquivo { get; set; }
    }
}
