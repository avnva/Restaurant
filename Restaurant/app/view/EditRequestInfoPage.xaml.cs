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
    /// Логика взаимодействия для EditRequestInfoPage.xaml
    /// </summary>
    public partial class EditRequestInfoPage : Page
    {
        public EditRequestInfoPage(Request request)
        {
            InitializeComponent();
            DataContext = new EditRequestViewModel(request);
            if (request == null)
            {
                // Скрываем кнопку удаления, если объект равен null
                Delete.Visibility = Visibility.Collapsed;
            }
        }
    }
}
