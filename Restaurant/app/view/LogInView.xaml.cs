using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    LogInViewModel viewModel = new LogInViewModel();
    public LogInView()
    {
        InitializeComponent();

        
        Closing += OnClose;
        DataContext = new LogInViewModel();
        (DataContext as ViewModelBase).Close += () => { Close(); };
        (DataContext as ViewModelBase).MessageBoxRequest +=
            ViewMessageBoxRequest;

        EventManager.RegisterClassHandler(typeof(Window), KeyDownEvent, new KeyEventHandler(MainKeyDown));

        languageText.Text = "Язык раскладки: " + Language.GetEquivalentCulture().DisplayName;
        capsLockPressed.Text = "Клавиша Caps Lock нажата";

        bool isCapsLockToggled = Keyboard.IsKeyToggled(Key.CapsLock);
        if (isCapsLockToggled)
            capsLockPressed.Text = "Клавиша Caps Lock нажата";
        else
            capsLockPressed.Text = "";

        InputLanguageManager.Current.InputLanguageChanged +=
               new InputLanguageEventHandler((sender, e) =>
               {
                   languageText.Text = "Язык раскладки: " + e.NewLanguage.DisplayName;
               });
    }

    private void LogInButton_Click(object sender, RoutedEventArgs e)
    {
        //try
        //{
            Cursor = Cursors.Wait;
            if (String.IsNullOrEmpty(loginTextBox.Text))
            {
                MessageBox.Show("Логин не заполнен. Введите логин");
                return;
            }

            if (String.IsNullOrEmpty(passwordBox.Password))
            {
                MessageBox.Show("Пароль не заполнен. Введите пароль");
                return;
            }

            var login = loginTextBox.Text;
            var password = passwordBox.Password;
            Cursor = Cursors.Wait;
            if (viewModel.AuthenticateUser(login, password))
            {
                // Успешная аутентификация - выполните необходимые действия
                //var mainWindow = new MainWindow(_dbContext, user);
                Cursor = Cursors.Arrow;
                //mainWindow.Show();
                Close();
                MessageBox.Show("Успешный вход!");
            }
            else
            {
                // Неудачная аутентификация - выполните необходимые действия
                MessageBox.Show("Неверные учетные данные.");
            }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show("Ошибка при загрузке программы");
        //}
        //finally
        //{
        //    Cursor = Cursors.Arrow;
        //}
    }


    private void ViewMessageBoxRequest(object sender, MessageBoxEventArgs e)
    {
        e.Show();
    }

    private void MainKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.CapsLock)
        {
            bool isCapsLockToggled = Keyboard.IsKeyToggled(Key.CapsLock);
            if (isCapsLockToggled)
                capsLockPressed.Text = "Клавиша Caps Lock нажата";
            else
                capsLockPressed.Text = "";
        }
    }

    private void OnClose(object sender, CancelEventArgs e)
    {
        Closing -= OnClose;
        (DataContext as ViewModelBase).MessageBoxRequest -= ViewMessageBoxRequest;
    }
}
