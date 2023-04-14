using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSVito
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CSViewModel contexto = new CSViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.contexto = this.DataContext as CSViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button fuente = (Button)sender;
            switch (fuente.Content)
            { 
                case "Seleccionar .CSV":
                    SeleccionarCSV();
                    break;
                case "Guardar":
                    this.contexto.CambiarMatricula();
                    break;
                case "Validar":
                    this.contexto.Validar();
                    break;
                case "+":
                    this.contexto.AgregarEstudiante();
                    break;
                case "X":
                    Estudiantes seleccionado = ((sender as Button).Tag as Estudiantes);
                    this.contexto.EliminarEstudiante(seleccionado);
                    break;
            }
        }

        private void EliminarEstudiante(Estudiantes seleccionado)
        {
            
        }

        private void SeleccionarCSV()
        {
            // Configure open file dialog box
            var dialog = new Microsoft.Win32.OpenFileDialog();
            
            dialog.DefaultExt = ".csv"; // Default file extension
            dialog.Filter = "CSV Files (.csv)|*.csv"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dialog.FileName;
                this.contexto.ObtenerPersonas(filename);
            }

        }
    }

}
