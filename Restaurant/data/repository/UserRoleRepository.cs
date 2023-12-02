using System.Collections.Generic;
using System.Linq;

namespace Restaurant.repository;

public class UserRoleRepository
{
    private readonly RestaurantDbContext _context;

    public UserRoleRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void AddUserRole(UserRole userRole)
    {
        _context.Roles.Add(userRole);
        _context.SaveChanges();
    }

    // READ
    public List<UserRole> GetRoles()
    {
        return _context.Roles.ToList();
    }

    public UserRole GetUserRoleById(int userRoleId)
    {
        return _context.Roles.Find(userRoleId);
    }

    // UPDATE
    public void UpdateUserRole(UserRole updatedUserRole)
    {
        var existingUserRole = _context.Roles.Find(updatedUserRole);

        if (existingUserRole != null)
        {
            existingUserRole.UserRoleName = updatedUserRole.UserRoleName;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void DeleteUserRole(int userRoleId)
    {
        var userRoleToDelete = _context.Roles.Find(userRoleId);

        if (userRoleToDelete != null)
        {
            _context.Roles.Remove(userRoleToDelete);
            _context.SaveChanges();
        }
    }
}
