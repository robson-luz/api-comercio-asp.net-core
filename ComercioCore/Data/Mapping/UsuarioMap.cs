using ComercioCore.Models.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.Data.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario", "dbo");

            builder.HasKey(u => u.IdUsuario);

            builder.Property(u => u.IdUsuario).IsRequired();
            builder.Property(u => u.Login).HasMaxLength(30).IsRequired();
            builder.Property(u => u.SenhaHash).HasMaxLength(64).IsRequired();
            builder.Property(u => u.SenhaSalt).HasMaxLength(128).IsRequired();

            builder.HasMany(u => u.Pessoas).WithOne(p => p.Usuario).HasForeignKey(p => p.IdUsuario);
        }
    }
}
