using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ComercioCore.Models.Categorias;

namespace ComercioCore.Models.Pessoas
{
    public static class PessoaQuery
    {
        public static IQueryable<Pessoa> ComId(this IQueryable<Pessoa> pessoas, int idPessoa)
        {
            if (idPessoa == 0)
            {
                return pessoas;
            }

            return pessoas.Where(p => p.IdPessoa == idPessoa);
        }

        public static IQueryable<Pessoa> ComCpf(this IQueryable<Pessoa> pessoas, string cpf)
        {
            if (String.IsNullOrEmpty(cpf))
            {
                return pessoas;
            }

            return pessoas.Where(p => p.Cpf == cpf);
        }

        public static IQueryable<Pessoa> ComEmail(this IQueryable<Pessoa> pessoas, string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return pessoas;
            }

            return pessoas.Where(p => p.Email == email);
        }

        public static IQueryable<Pessoa> ComIdUsuario(this IQueryable<Pessoa> pessoas, int idUsuario)
        {
            if (idUsuario == 0)
            {
                return pessoas;
            }

            return pessoas.Where(p => p.IdUsuario == idUsuario);
        }

        public static IQueryable<Pessoa> OndeNomeContem(this IQueryable<Pessoa> pessoas, string nome)
        {
            if (String.IsNullOrEmpty(nome))
            {
                return pessoas;
            }

            return pessoas.Where(p => p.Nome.Contains(nome));
        }
    }
}
