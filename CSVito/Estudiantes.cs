using FluentValidation.Results;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace CSVito
{
    internal class Estudiantes : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public int Id {set; get;}
        private string matricula;
        public String Matricula 
        {
            set
            { 
                this.matricula = value;
                ValidarMatricula();
                OnPropertyChanged();
            }
            get { return this.matricula; }
        }

        private string nombre;
        public String Nombre 
        {
            set
            {
                this.nombre = value;
                LimpiarErrores(nameof(Matricula));
                if (String.IsNullOrWhiteSpace(nombre))
                {
                    AgregarError(nameof(Nombre), "Todos los campos son obligatorios...");
                    Console.WriteLine("nombre vacia");
                }
            }
            get { return this.nombre; }
        }
        public String ApellidoPaterno { set; get; }
        public String ApellidoMaterno { set; get; }
        public String CorreoInstitucional { set; get; }
        public String CorreoPersonal { set; get; }

        private bool valido;
        public bool Valido 
        {
            set
            { 
                this.valido = value;
                OnPropertyChanged();
            }
            get { return this.valido; } 
        }

        public ObservableCollection<ValidationFailure> Errores { set; get; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public String ToString()
        {
            return $"{Matricula} | {Nombre} {ApellidoPaterno} {ApellidoMaterno}";

        }

        public void Validar()
        {
            ValidarMatricula();
        }

        private readonly Dictionary<string, List<string>> erroresMatricula = new Dictionary<string, List<string>>();
        public bool HasErrors => erroresMatricula.Any();
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public IEnumerable GetErrors(string propertyName)
        {
            List<string> errores = new List<string>();
            if (propertyName == null) return errores;
            if (this.erroresMatricula.TryGetValue(propertyName, out List<string> erroresEncontrados))
            {
                errores = erroresEncontrados;
            }
            return errores;
        }
        public void AgregarError(string nombrePropiedad, string mensajeError)
        {
            if (!erroresMatricula.ContainsKey(nombrePropiedad))
            {
                erroresMatricula.Add(nombrePropiedad, new List<string>());
            }
            erroresMatricula[nombrePropiedad].Add(mensajeError);
            ErroresCambiaron(nombrePropiedad);
        }
        private void ErroresCambiaron(string nombrePropiedad)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nombrePropiedad));
        }
        private void LimpiarErrores(string nombrePropiedad)
        {
            if (erroresMatricula.Remove(nombrePropiedad))
            {
                ErroresCambiaron(nombrePropiedad);
            }
        }

        private void ValidarMatricula()
        {
            LimpiarErrores(nameof(Matricula));
            if (matricula == null || String.IsNullOrWhiteSpace(matricula))
            {
                AgregarError(nameof(Matricula), "● Todos los campos son obligatorios");
                return;
            }
            if (matricula.Count() != 9 )
            {
                AgregarError(nameof(Matricula), $"● La matrícula debe contener 9 números.");
            }
            long matriculaNumerica = 0;
            if (!long.TryParse(matricula, out matriculaNumerica))
            {
                AgregarError(nameof(Matricula), $"● La matrícula sólo puede contener números. " +
                $"\n   El sufijo 'S' se agregará automáticamente al " +
                $"\n   registrar el estudiante en la base de datos.");
            }
        }
    }
}
