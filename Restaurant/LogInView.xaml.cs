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

namespace Restaurant;

public partial class LogInView : Window
{
    public LogInView()
    {
        InitializeComponent();
        LogInViewModel viewModel = new LogInViewModel();
        DataContext = new LogInViewModel();

        viewModel.CurrentKeyboardLayout = "Your Keyboard Layout";
        viewModel.CapsLockPressed = "Caps Lock is Pressed";
    }
}
