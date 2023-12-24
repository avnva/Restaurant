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
    /// Логика взаимодействия для EditOrderInfoPage.xaml
    /// </summary>
    public partial class EditOrderInfoPage : Page
    {
        public EditOrderInfoPage(Order order)
        {
            InitializeComponent();
            DataContext = new EditOrderViewModel(order);
            if (order == null)
            {
                // Скрываем кнопку удаления, если объект Order равен null
                Delete.Visibility = Visibility.Collapsed;
            }
        }

        private void PrintCommandButton_Click(object sender, RoutedEventArgs e)
        {
            ExportDocumentWindow exportDocument = new ExportDocumentWindow();
        }
    }
}
