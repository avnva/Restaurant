using Microsoft.EntityFrameworkCore;
using Restaurant.app.view_model;
using System;
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

namespace Restaurant.app.view
{
    /// <summary>
    /// Логика взаимодействия для SuppliesPage.xaml
    /// </summary>
    public partial class SuppliesPage : Page
    {
        public SuppliesPage()
        {
            InitializeComponent();

            DataContext = new SuppliesPageViewModel();
        }
        private void PrintCommand_Click(object sender, RoutedEventArgs e)
        {
            var ExportdocumentWindow = new ExportDocumentWindow(dataGrid);
            //ExportdocumentWindow.ShowDialog();
        }
    }
}
