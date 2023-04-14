using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CSVito
{
    internal class CSViewModel
    {
        public ICollectionView Personas { get; set; }
        private ObservableCollection<Estudiantes> personasCollection { get; set; }
        private int Id = 0;
        public CSViewModel() 
        {
            this.personasCollection = new ObservableCollection<Estudiantes>();
            this.Personas = CollectionViewSource.GetDefaultView(personasCollection);
        }
        internal void AgregarEstudiante()
        {
            Estudiantes nuevoEstudiante = new Estudiantes();
            nuevoEstudiante.Validar();
            this.personasCollection.Add(nuevoEstudiante);
        }

        internal void EliminarEstudiante(Estudiantes seleccionado)
        {
            this.personasCollection.Remove(seleccionado);
        }

        public void Validar()
        {
            foreach (Estudiantes persona in this.personasCollection)
            {
                persona.Validar();
            }
        }

        public void CambiarMatricula()
        {
            foreach (Estudiantes estudiante in this.personasCollection)
            {
                estudiante.Matricula = "asdf";
                Console.WriteLine(estudiante.ToString());
            }
        }

        public void ObtenerPersonas(String ruta)
        {
            using (var reader = new StreamReader(ruta))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<EstudiantesMap>();
                var records = csv.GetRecords<Estudiantes>();
                foreach (Estudiantes estudiante in records)
                {
                    personasCollection.Add(estudiante);
                    estudiante.Validar();
                }

            }

            
        }

        public void Guardar()
        {
            foreach (Estudiantes persona in personasCollection)
            { 
                Console.WriteLine(persona.ToString());
            }

            var ejemplos = new List<Estudiantes>
            {
                new Estudiantes
                {
                    Matricula="20000001",
                    Nombre="Pablo",
                    ApellidoPaterno="Salazar",
                    ApellidoMaterno = "Gutierrez",
                    CorreoInstitucional = "ASDF@ASDF.COM",
                    CorreoPersonal = "QWER@QWER.NET"
                },
            };


        }

        public sealed class EstudiantesMap : ClassMap<Estudiantes>
        {
            public EstudiantesMap()
            {
                AutoMap(CultureInfo.InvariantCulture);
                Map(e => e.Valido).Ignore();
                Map(e => e.Id).Ignore();
            }
        }

    }
}
