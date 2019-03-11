using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.ViewModel
{
    public class PaginacaoViewModel
    {
        public int Inicio { get; set; }

        public int Limite { get; set; }

        public int TotalRegistros { get; set; }
    }
}
