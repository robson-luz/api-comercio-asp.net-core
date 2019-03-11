using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.ViewModel.Pedidos
{
    public class ItemViewModel
    {
        [Range(1, Int32.MaxValue, ErrorMessage = "Campo '{0}' é obrigatório.")]
        public int IdProduto { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Campo '{0}' é obrigatório.")]
        public int Quantidade { get; set; }

        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Campo '{0}' é obrigatório.")]
        public decimal Subtotal { get; set; }

    }
}
