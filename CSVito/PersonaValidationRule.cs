using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace CSVito
{
    internal class PersonaValidationRule : ValidationRule
    {
        private readonly Regex matriculaRegex = new Regex("^[0-9]{9}$");
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            
            
        String matricula = value as String;

            if (matricula == null)
            {
                return new ValidationResult(false,
                    "Todos los campos son obligatorios.");
            }
            if (!MatriculaEsValida(matricula))
            {
                return new ValidationResult(false,
                    "La matrícula debe contener 9 números únicamente." +
                    "\nEl prefijo 'S' se agregará automáticamente, al registrar el estudiante.");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }

        private bool MatriculaEsValida(string matricula)
        {
            return matriculaRegex.IsMatch(matricula);
        }

        private bool ValidarAlfabetico(String valor)
        {
            Regex reges = new Regex("^[a-zA-Z]*$");
            bool resultado = true;
            if (valor != null)
                resultado = reges.IsMatch(valor);
            return resultado;
        }
    }
}
