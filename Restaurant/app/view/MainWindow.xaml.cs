using Restaurant;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataStore.Frame = mainFrame;
        }

        private void Dishes_Click(object sender, RoutedEventArgs e)
        {
            DishesPage dishesPage = new DishesPage();
            mainFrame.NavigationService.Navigate(dishesPage);
        }
        private void Warehouses_Click(object sender, RoutedEventArgs e)
        {
            WarehousesPage warehousesPage = new WarehousesPage();
            mainFrame.NavigationService.Navigate(warehousesPage);
        }
        private void Supplies_Click(object sender, RoutedEventArgs e)
        {
            SuppliesPage suppliesPage = new SuppliesPage();
            mainFrame.NavigationService.Navigate(suppliesPage);
        }
        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            OrdersPage ordersPage = new OrdersPage();
            mainFrame.NavigationService.Navigate(ordersPage);
        }
        private void Requests_Click(object sender, RoutedEventArgs e)
        {
            RequestsPage requestsPage = new RequestsPage();
            mainFrame.NavigationService.Navigate(requestsPage);
        }
        public void ContentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process process = new();
                process.StartInfo = new ProcessStartInfo("Help.pdf")
                {
                    UseShellExecute = true
                };
                process.Start();
            }
            catch (Exception)
            {
                MessageBoxEventArgs args = new MessageBoxEventArgs(null, "Ошибка при открытии руководства пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                args.Show();
            }

        }
        private void AboutProgram_Click(object sender, RoutedEventArgs e)
        {
            AboutProgram aboutProgramPage = new AboutProgram();
            mainFrame.NavigationService.Navigate(aboutProgramPage);
        }
    }
}
