using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.ViewModel.Usuarios
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "Login não informado.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Digite uma senha.")]
        public string Senha { get; set; }
    }
}
