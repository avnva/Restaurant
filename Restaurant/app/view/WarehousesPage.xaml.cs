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
    /// Логика взаимодействия для WarehousesPage.xaml
    /// </summary>
    public partial class WarehousesPage : Page
    {
        public WarehousesPage()
        {
            InitializeComponent();
            DataContext = new WarehousesPageViewModel();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                if (dataGrid.SelectedItem is Warehouse selectedWarehouse)
                {
                    // Передать выбранный склад во ViewModel
                    WarehousesPageViewModel viewModel = (WarehousesPageViewModel)DataContext;
                    viewModel.SelectedWarehouse = selectedWarehouse;

                    // Теперь у вас есть выбранный склад в вашей ViewModel
                }
            }
        }
    }
}
