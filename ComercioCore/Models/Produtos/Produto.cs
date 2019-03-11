using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComercioCore.Models.Categorias;
using ComercioCore.Models.Itens;

namespace ComercioCore.Models.Produtos
{
    public class Produto
    {
        public int IdProduto { get; set; }

        public int IdCategoria { get; set; }

        public string Descricao { get; set; }

        public decimal Preco { get; set; }

        public string DiretorioImagem { get; set; }


        public virtual Categoria Categoria { get; set; }
        public virtual ICollection<Item> Itens { get; set; }
    }
}
