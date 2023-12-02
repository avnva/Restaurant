using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.repository;

public class UserRepository
{
    private readonly RestaurantDbContext _context;

    public UserRepository(RestaurantDbContext context)
    {
        _context = context;
    }
    
    // В domain слое логика регистрации и добавления юзера, сюда просто отправляем уже юзера
    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public List<User> GetUsers()
    {
        return _context.Users.ToList();
    }

    public User GetUserById(int userId)
    {
        return _context.Users.Find(userId) ?? throw new InvalidOperationException();
    }

    public void UpdateUser(User updatedUser)
    {
        var existingUser = _context.Users.Find(updatedUser);

        if (existingUser == null) return;
        
        existingUser.UserId = updatedUser.UserId;
        existingUser.Login = updatedUser.Login;
        existingUser.PasswordHash = updatedUser.PasswordHash;
        existingUser.UserRole = updatedUser.UserRole;

        _context.SaveChanges();
    }

    public void DeleteUser(int userId)
    {
        var userToDelete = _context.Users.Find(userId);

        if (userToDelete != null)
        {
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
        }
    }
}