using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

namespace JsonEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel ViewModel { get; set; } = new ViewModel();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            ViewModel = new ViewModel();
            InvalidateVisual();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "JSON files|*.json|All files|*.*";
            if (dialog.ShowDialog() != true) return;

            using var reader = File.OpenText(dialog.FileName);
            using var jsonReader = new JsonTextReader(reader);
            ViewModel.Json = JObject.Load(jsonReader);
            ViewModel.JsonString = ViewModel.Json.ToString();
        }

        private void Save(bool compact)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e) => Save(false);

        private void SaveCompact_Click(object sender, RoutedEventArgs e) => Save(true);

        private void Exit_Click(object sender, RoutedEventArgs e) => Close();

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!ViewModel.IsChanged()) return;
            var confirm = MessageBox.Show("Are you sure you want to exit? Your unsaved changes will be lost.",
                "Confirm Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirm != MessageBoxResult.Yes)
                e.Cancel = true;
        }
    }
}
