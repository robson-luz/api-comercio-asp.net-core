using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ComercioCore.Models.Usuarios
{
    public static class UsuarioQuery
    {
        public static IQueryable<Usuario> ComId(this IQueryable<Usuario> usuarios, int idUsuario)
        {
            if (idUsuario == 0)
            {
                return usuarios;
            }

            return usuarios.Where(u => u.IdUsuario == idUsuario);
        }

        public static IQueryable<Usuario> ComLogin(this IQueryable<Usuario> usuarios, string login)
        {
            if (String.IsNullOrEmpty(login))
            {
                return usuarios;
            }

            return usuarios.Where(u => u.Login == login);
        }
    }
}
