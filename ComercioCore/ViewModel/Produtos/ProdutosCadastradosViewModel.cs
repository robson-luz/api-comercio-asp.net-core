using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.ViewModel.Produtos
{
    public class ProdutosCadastradosViewModel
    {
        public int IdCategoria { get; set; }
        public string Descricao { get; set; }

        public PaginacaoViewModel Paginacao { get; set; }

        public ProdutosCadastradosViewModel()
        {
            Paginacao = new PaginacaoViewModel();
        }
    }
}
