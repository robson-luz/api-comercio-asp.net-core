using ComercioCore.Models.Pessoas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.Data.Mapping
{
    public class PessoaMap : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoa", "dbo");

            builder.HasKey(p => p.IdPessoa);

            builder.Property(p => p.IdPessoa).IsRequired();
            builder.Property(p => p.IdUsuario).IsRequired();
            builder.Property(p => p.TipoPessoa).HasMaxLength(1).IsRequired();
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Cpf).HasMaxLength(14).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(30).IsRequired();
            builder.Property(p => p.NumeroCelular).HasMaxLength(14);
            builder.Property(p => p.Foto);


            builder.HasOne(p => p.Usuario).WithMany(c => c.Pessoas).HasForeignKey(p => p.IdUsuario);
        }
    }
}
