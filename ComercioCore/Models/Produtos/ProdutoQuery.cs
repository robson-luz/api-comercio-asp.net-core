using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComercioCore.Models.Categorias;

namespace ComercioCore.Models.Produtos
{
    public static class ProdutoQuery
    {
        public static IQueryable<Produto> ComId(this IQueryable<Produto> produtos, int idProduto)
        {
            if (idProduto == 0)
            {
                return produtos;
            }

            return produtos.Where(p => p.IdProduto == idProduto);
        }

        public static IQueryable<Produto> ComDescricao(this IQueryable<Produto> produtos, string descricao)
        {
            if (String.IsNullOrEmpty(descricao))
            {
                return produtos;
            }

            return produtos.Where(p => p.Descricao == descricao);
        }

        public static IQueryable<Produto> ComIdCategoria(this IQueryable<Produto> produtos, int idCategoria)
        {
            if (idCategoria == 0)
            {
                return produtos;
            }

            return produtos.Where(p => p.IdCategoria == idCategoria);
        }

        public static IQueryable<Produto> OndeDescricaoContem(this IQueryable<Produto> produtos, string descricao)
        {
            if (String.IsNullOrEmpty(descricao))
            {
                return produtos;
            }

            return produtos.Where(p => p.Descricao.Contains(descricao));
        }
    }
}
