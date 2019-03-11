using ComercioCore.Data.Mapping;
using ComercioCore.Models.Categorias;
using ComercioCore.Models.Itens;
using ComercioCore.Models.Pedidos;
using ComercioCore.Models.Pessoas;
using ComercioCore.Models.Produtos;
using ComercioCore.Models.Usuarios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.Data
{
    public class ComercioDbContext : DbContext
    {
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Item> Item { get; set; }

        public ComercioDbContext(DbContextOptions<ComercioDbContext> options):base(options) 
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new PessoaMap());
            modelBuilder.ApplyConfiguration(new PedidoMap());
            modelBuilder.ApplyConfiguration(new ItemMap());
        }

    }
}
