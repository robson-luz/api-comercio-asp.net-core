using ComercioCore.Models.Pessoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.Models.Usuarios
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Login { get; set; }
        public byte[] SenhaHash { get; set; }
        public byte[] SenhaSalt { get; set; }

        public virtual ICollection<Pessoa> Pessoas { get; set; }
    }
}
