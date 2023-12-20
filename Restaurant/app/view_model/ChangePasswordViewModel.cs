using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using Restaurant.data.repository;

namespace Restaurant.app.view_model;

public class ChangePasswordViewModel : ViewModelBase
{
    private int id;
    private string previousPassword;
    private string newPassword1;
    private string newPassword2;

    private AuthentificatorRepository authentificatorRepository;
    //private readonly INavigationService navigationService;

    private RelayCommand saveCommand;

    public ChangePasswordViewModel(int id)
    {
        this.id = id;
        previousPassword = string.Empty;
        newPassword1 = string.Empty;
        newPassword2 = string.Empty;
        authentificatorRepository = new AuthentificatorRepository(new RestaurantDbContext());
        //navigationService = new NavigationService(Application.Current.MainWindow);
    }

    public RelayCommand SaveCommand
    {
        get
        {
            return saveCommand ??= new RelayCommand(UpdatePassword, IsValidPasswordData);
        }
    }

    public string PreviousPassword
    {
        get => previousPassword;
        set
        {
            previousPassword = value;
            OnPropertyChanged("Email");
        }
    }

    public string NewPassword1
    {
        get => newPassword1;
        set
        {
            newPassword1 = value;
            OnPropertyChanged("Password");
        }
    }

    public string NewPassword2
    {
        get => newPassword2;
        set
        {
            newPassword2 = value;
            OnPropertyChanged("Password");
        }
    }


    public bool IsValidPasswordData(object email)
    {
        return NewPassword1 == NewPassword2 && NewPassword1 != "" && NewPassword2 != "" && PreviousPassword != "";
    }

    public void UpdatePassword(object employee)
    {
        bool result = authentificatorRepository.ChangePassword(id, previousPassword, newPassword1);

        if (result)
        {
            MessageBox.Show("Пароль успешно изменен", "Смена пароля", MessageBoxButton.OK, MessageBoxImage.Information);
            //navigationService.CloseCurrentWindow();
        }
        else
        {
            MessageBox.Show("Не удалось сменить пароль. Проверьте правильность ввода текущего пароля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

