using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
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
using System.IO;
//using Microsoft.Win32;
namespace Sergioteacher.Csharp05.EditorTextoA
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static String tituloA = "EditorTextoA";
        private static String fpath;
        bool modificado;
        /// <summary>
        /// Función principal.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.Title = tituloA;
            fpath = "";
            modificado = false;
        }
        //  ########################################################################
        /// <summary>
        /// Atrapando el evento Clics desde el Menu -> Acerca de
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Acercade_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Un editor, con un control básico de edición \n " +
                "mostrando:\n" +
                "   -Un menú con `Command´ \n" +
                "   -y facilidades de fichero con Win32" +
                "\n" +
                "\n" +
                "             Copyright (C) Sergioteacher", "Edidor básico");
        }
        /// <summary>
        /// Atrapando el evento Clics desde el Menu -> Salir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Ventana1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (modificado)
            {
                MessageBoxResult resultado = MessageBox.Show("Se ha modificado, ¿Guardar?", "Duda...", MessageBoxButton.YesNo);
                switch (resultado)
                {
                    case MessageBoxResult.Yes:
                        GuardarArchivo();
                        break;
                    case MessageBoxResult.No:
                        MessageBox.Show("Se sale solo", "Borrando...");
                        break;
                }
            }
        }
      
        private void GuardarArchivo()
        {
            if (fpath == "")
            {
                fpath = InputBox("Guardar archivo", "Introduce la ruta y nombre del archivo:", "");
                if (fpath == "")
                {
                    MessageBox.Show("Se canceló la operación", "Cancelado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
            }

            File.WriteAllText(fpath, Tedit.Text);
            modificado = false;
            Ventana1.Title = $"{tituloA} {fpath}";
        }
        //  ########################################################################
        /// <summary>
        /// Acepta la captura del evento New
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_CanExecute_New(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        /// <summary>
        /// Implementación específica de la llamada a "Command" desde New
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_Executed_New(object sender, ExecutedRoutedEventArgs e)
        {
            if (modificado)
            {
                MessageBoxResult resultado = MessageBox.Show("Se ha modificado, ¿Guardar?", "Duda...", MessageBoxButton.YesNoCancel);
                switch (resultado)
                {
                    case MessageBoxResult.Yes:
                        GuardarArchivo();
                        break;
                    case MessageBoxResult.No:
                        Tedit.Text = "";
                        fpath = "";
                        modificado = false;
                        Ventana1.Title = $"{tituloA} {fpath}";
                        break;
                    case MessageBoxResult.Cancel:
                        MessageBox.Show("Cancelado -> No se guardará", "Sin cambios");
                        break;
                }
            }
            else
            {
                Tedit.Text = "";
                fpath = "";
                modificado = false;
                Ventana1.Title = $"{tituloA} {fpath}";
            }
        }
        /// <summary>
        /// Acepta la captura del evento Open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_CanExecute_Open(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        /// <summary>
        /// Implementación específica de la llamada a "Command" desde Open
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_Executed_Open(object sender, ExecutedRoutedEventArgs e)
        {
            if (modificado)
            {
                MessageBoxResult resultado = MessageBox.Show("Se ha modificado, ¿Guardar?", "Duda...", MessageBoxButton.YesNo);
                switch (resultado)
                {
                    case MessageBoxResult.Yes:
                        GuardarArchivo();
                        break;
                }
            }

            string filePath = InputBox("Abrir archivo", "Introduce la ruta y nombre del archivo a abrir:", "");
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    Tedit.Text = File.ReadAllText(filePath);
                    modificado = false;
                    fpath = filePath;
                    Ventana1.Title = $"{tituloA} {fpath}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al abrir el archivo: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        /// <summary>
        /// Acepta la captura del evento Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        /// <summary>
        /// Implementación específica de la llamada a "Command" desde Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_Executed_Save(object sender, ExecutedRoutedEventArgs e)
        {
            GuardarArchivo();
        }
        /// <summary>
        /// Acepta la captura del evento GuardarEn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_CanExecute_GuardarEn(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        /// <summary>
        /// Acepta la captura del evento GuardarEn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_Executed_GuardarEn(object sender, ExecutedRoutedEventArgs e)
        {
            GuardarArchivo();
        }
        /// <summary>
        /// Acepta la captura del evento Print
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_CanExecute_Print(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        /// <summary>
        /// Implementación específica de la llamada a "Command" desde Print
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_Executed_Print(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Imprimiendo ...");
        }
        /// <summary>
        /// Acepta la captura del evento Help
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_CanExecute_Help(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        /// <summary>
        /// Acepta la captura del evento Help
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBinding_Executed_Help(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Help");
        }

        private void Tedit_TextChanged(object sender, TextChangedEventArgs e)
        {
            modificado = true;
            Ventana1.Title = $"{tituloA} {(modificado ? "*" : "")}{fpath}";
        }

        private void Tedit_KeyUp(object sender, KeyEventArgs e)
        {
            int fila = Tedit.GetLineIndexFromCharacterIndex(Tedit.CaretIndex);
            int columna = Tedit.CaretIndex - Tedit.GetCharacterIndexFromLineIndex(fila);
            Testado.Text = $"Fila: {fila + 1}, Columna: {columna + 1}";
        }

        private string InputBox(string title, string promptText, string defaultValue)
        {
            string value = defaultValue;
            string input = Microsoft.VisualBasic.Interaction.InputBox(promptText, title, defaultValue);
            if (input != "")
            {
                value = input;
            }
            return value;
        }
    }
}
