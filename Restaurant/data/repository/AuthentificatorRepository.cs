using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.data.repository;

public class AuthentificatorRepository
{
    private readonly RestaurantDbContext _context;

    public AuthentificatorRepository(RestaurantDbContext context)
    {
        _context = context;
    }
    public int FindEmployee(string login, string password)
    {

        int id = _context.Users.Where(e => e.Login == login && e.PasswordHash == BCrypt.Net.BCrypt.HashPassword(password))
                    .Select(e => e.UserID)
                    .FirstOrDefault();
        return id;
    }

    public bool ChangePassword(int id, string previousPassword, string newPassword)
    {
        User user = _context.Users.FirstOrDefault(u => u.UserID == id);

        if (user != null && user.PasswordHash == BCrypt.Net.BCrypt.HashPassword(previousPassword))
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _context.SaveChanges();
            return true;
        }

        return false;
    }
}
