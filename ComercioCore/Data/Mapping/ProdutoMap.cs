using ComercioCore.Models.Produtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.Data.Mapping
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto", "dbo");

            builder.HasKey(p => p.IdProduto);

            builder.Property(p => p.IdProduto).IsRequired();
            builder.Property(p => p.IdCategoria).IsRequired();
            builder.Property(p => p.Descricao).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Preco).HasColumnType("decimal(13,2)").IsRequired();
            builder.Property(p => p.DiretorioImagem).HasMaxLength(100);

            builder.HasOne(p => p.Categoria).WithMany(c => c.Produtos).HasForeignKey(p => p.IdCategoria);
            builder.HasMany(p => p.Itens).WithOne(i => i.Produto).HasForeignKey(i => i.IdProduto);
        }
    }
}
