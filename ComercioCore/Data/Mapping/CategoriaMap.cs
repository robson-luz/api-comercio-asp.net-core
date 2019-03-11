using ComercioCore.Models.Categorias;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.Data.Mapping
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria","dbo");

            builder.HasKey(c => c.IdCategoria);

            builder.Property(c => c.IdCategoria).IsRequired();
            builder.Property(c => c.Descricao).HasMaxLength(20).IsRequired();

            builder.HasMany(c => c.Produtos).WithOne(p => p.Categoria).HasForeignKey(p => p.IdCategoria);
        }
    }
}
