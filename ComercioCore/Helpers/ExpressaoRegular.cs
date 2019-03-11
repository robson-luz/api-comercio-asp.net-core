using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComercioCore.Helpers
{
    public class ExpressaoRegular
    {
        public const string CPF = @"([0-9]{3}\.){2}[0-9]{3}-[0-9]{2}";
        public const string RG = @"[0-9]\.[0-9]{3}\.[0-9]{3}-[0-9]";
        public const string CEP = @"^\d{5}-\d{3}$";
        public const string EMAIL = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        public const string CELULAR = @"\(\d{2}\)\d{5}-\d{4}";
        public const string TELEFONE = @"\(\d{2}\)\d{4}-\d{4}";
    }
}
