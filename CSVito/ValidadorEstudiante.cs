using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSVito
{
    internal class ValidadorEstudiante : AbstractValidator<Estudiantes>
    {
        public ValidadorEstudiante() 
        {
            RuleFor(estudiante => estudiante.Matricula)
                .NotEmpty().WithMessage("No puede estar vacío")
                .MaximumLength(9).WithMessage("No debe exceder 50 caracteres.");
            RuleFor(estudiante => estudiante.Nombre)
                .NotEmpty().WithMessage("No puede estar vacío")
                .MaximumLength(50).WithMessage("No debe exceder 50 caracteres.")
                .Must(Validador.ValidarAlfabetica).WithMessage("No puede tener números ni signos.");
            RuleFor(estudiante => estudiante.ApellidoPaterno)
                .NotEmpty().WithMessage("No puede estar vacío")
                .MaximumLength(50).WithMessage("No debe exceder 50 caracteres.")
                .Must(Validador.ValidarAlfabetica).WithMessage("No puede tener números ni signos.");

        }

    }
}
