using ComercioCore.Models.Pedidos;
using ComercioCore.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.Models.Pessoas
{
    public class Pessoa
    {
        public int IdPessoa { get; set; }

        public int IdUsuario { get; set; }

        public char TipoPessoa { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public string NumeroCelular { get; set; }

        public byte[] Foto { get; set; }


        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
