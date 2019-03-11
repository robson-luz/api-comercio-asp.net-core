using ComercioCore.Models.Itens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.Data.Mapping
{
    public class ItemMap : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Item", "dbo");

            builder.HasKey(i => i.IdItem);

            builder.Property(i => i.IdItem).IsRequired();
            builder.Property(i => i.IdProduto).IsRequired();
            builder.Property(i => i.IdPedido).HasMaxLength(1).IsRequired();
            builder.Property(i => i.Quantidade).IsRequired();
            builder.Property(i => i.Subtotal).HasColumnType("decimal(13,2)").IsRequired();

            builder.HasOne(i => i.Pedido).WithMany(p => p.Itens).HasForeignKey(i => i.IdPedido);
            builder.HasOne(i => i.Produto).WithMany(p => p.Itens).HasForeignKey(i => i.IdProduto);
        }
    }
}
