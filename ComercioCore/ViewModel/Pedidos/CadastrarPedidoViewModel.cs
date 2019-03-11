using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.ViewModel.Pedidos
{
    public class CadastrarPedidoViewModel
    {
        [Range(1, Int32.MaxValue, ErrorMessage = "Campo '{0}' é obrigatório.")]
        public int IdPessoa { get; set; }

        [Required(ErrorMessage = "Campo '{0}' é obrigatório.")]
        public decimal Total { get; set; }

        public ICollection<ItemViewModel> Itens { get; set; }

    }
}
