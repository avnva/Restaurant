using Restaurant.app.view_model;
using System.Windows.Controls;

namespace Restaurant.app.view
{
    /// <summary>
    /// Логика взаимодействия для RequestsPage.xaml
    /// </summary>
    public partial class RequestsPage : Page
    {
        public RequestsPage()
        {
            InitializeComponent();

            DataContext = new RequestsPageViewModel();
        }
    }
}
