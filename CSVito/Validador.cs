using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSVito
{
    internal class Validador
    {
        private static readonly Regex alfabetico = new Regex("^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]*$");
        public static bool ValidarAlfabetica(String valor)
        {
            bool resultado = true;
            if (valor != null)
                resultado = alfabetico.IsMatch(valor);
            return resultado;
        }
    }
}
