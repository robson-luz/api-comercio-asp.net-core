using ComercioCore.Models.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.Models.Categorias
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}
