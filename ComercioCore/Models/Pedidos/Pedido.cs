using ComercioCore.Models.Itens;
using ComercioCore.Models.Pessoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.Models.Pedidos
{
    public class Pedido
    {
        public int IdPedido { get; set; }

        public int IdPessoa { get; set; }

        public DateTime DataPedido { get; set; }

        public decimal ValorTotal { get; set; }


        public virtual Pessoa Pessoa { get; set; }

        public virtual ICollection<Item> Itens { get; set; }
    }
}
