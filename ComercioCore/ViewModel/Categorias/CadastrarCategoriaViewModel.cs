using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.ViewModel.Categorias
{
    public class CadastrarCategoriaViewModel
    {
        [Required(ErrorMessage = "Campo '{0}' é obrigatório.")]
        [MaxLength(20, ErrorMessage = "Campo '{0}' deve ter no máximo {1} caracteres.")]
        public string Descricao { get; set; }
    }
}
