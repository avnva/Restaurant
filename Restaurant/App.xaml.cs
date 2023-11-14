using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Restaurant;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Здесь вы можете настроить ваше приложение, вызвать окно входа и т.д.
        // ...

        // Пример запуска главного окна
        //var mainWindow = new MainWindow();
        //mainWindow.Show();
    }
}
