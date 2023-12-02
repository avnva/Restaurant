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
    private readonly RestaurantDbContext _dbContext;

    public LogInViewModel()
    {
        _dbContext = new RestaurantDbContext();
    }

    public bool AuthenticateUser(string username, string password)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Login == username);

        return user != null && CheckPasswordHash(password, user.PasswordHash);
    }
    
    private bool CheckPasswordHash(string password, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }
}