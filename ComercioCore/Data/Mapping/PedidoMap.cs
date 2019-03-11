using ComercioCore.Models.Pedidos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.Data.Mapping
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido", "dbo");

            builder.HasKey(p => p.IdPedido);

            builder.Property(p => p.IdPedido).IsRequired();
            builder.Property(p => p.IdPessoa).IsRequired();
            builder.Property(p => p.DataPedido).IsRequired();
            builder.Property(p => p.ValorTotal).HasColumnType("decimal(13,2)").IsRequired();

            builder.HasOne(p => p.Pessoa).WithMany(pe => pe.Pedidos).HasForeignKey(p => p.IdPessoa);
            builder.HasMany(p => p.Itens).WithOne(i => i.Pedido).HasForeignKey(i => i.IdPedido);
        }
    }
}
