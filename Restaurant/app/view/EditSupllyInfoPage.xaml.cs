using Restaurant.app.model;
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
    /// Логика взаимодействия для EditSupllyInfoPage.xaml
    /// </summary>
    public partial class EditSupllyInfoPage : Page
    {
        public EditSupllyInfoPage(Supply supply)
        {
            InitializeComponent();
            DataContext = new EditSupplyViewModel(supply);
        }
    }
}
