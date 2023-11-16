using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Restaurant;

public class LogInViewModel : ViewModelBase
{
    private string _login;

    private RelayCommand _logInCommand;
    private string _password;
    private readonly RestaurantDbContext dbContext;

    public LogInViewModel()
    {
        dbContext = new RestaurantDbContext();
    }

    public bool AuthenticateUser(string username, string password)
    {
        // Получаем пользователя из базы данных по логину
        var user = dbContext.Users.FirstOrDefault(u => u.Login == username);

        // Проверяем, существует ли пользователь и совпадает ли хэш пароля
        if (user != null && CheckPasswordHash(password, user.PasswordHash))
        {
            // Успешная аутентификация
            return true;
        }

        // Неудачная аутентификация
        return false;
    }
    private bool CheckPasswordHash(string password, string storedHash)
    {
        // Реализуйте логику проверки хэша пароля (например, с использованием библиотеки для хэширования)
        // Важно не хранить пароли в открытом виде в базе данных
        // В реальном приложении следует использовать хэширование паролей и возможно, соль
        // Вам может потребоваться сторонняя библиотека для хэширования паролей (например, BCrypt.Net)
        // Пример:
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }

}