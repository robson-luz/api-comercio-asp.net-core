using ComercioCore.Models.Pedidos;
using ComercioCore.Models.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.Models.Itens
{
    public class Item
    {
        public int IdItem { get; set; }

        public int IdProduto { get; set; }

        public int IdPedido { get; set; }

        public int Quantidade { get; set; }

        public decimal Subtotal { get; set; }


        public virtual Produto Produto { get; set; }

        public virtual Pedido Pedido { get; set; }
    }
}
