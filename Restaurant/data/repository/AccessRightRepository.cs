using System.Collections.Generic;
using System.Linq;
using Restaurant.app.model;

namespace Restaurant.repository;

public class AccessRightRepository
{
    private readonly RestaurantDbContext _context;

    public AccessRightRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void AddAccessRight(AccessRight accessRight)
    {
        _context.AccessRights.Add(accessRight);
        _context.SaveChanges();
    }

    // READ
    public List<AccessRight> GetAccessRights()
    {
        return _context.AccessRights.ToList();
    }

    public AccessRight GetAccessRightById(int accessRightId)
    {
        return _context.AccessRights.Find(accessRightId);
    }

    // UPDATE
    public void UpdateAccessRight(AccessRight updatedAccessRight)
    {
        var existingAccessRight = _context.AccessRights.Find(updatedAccessRight);

        if (existingAccessRight != null)
        {
            existingAccessRight.Read = updatedAccessRight.Read;
            existingAccessRight.Write = updatedAccessRight.Write;
            existingAccessRight.Edit = updatedAccessRight.Edit;
            existingAccessRight.Delete = updatedAccessRight.Delete;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void DeleteAccessRight(int accessRightId)
    {
        var accessRightToDelete = _context.AccessRights.Find(accessRightId);

        if (accessRightToDelete != null)
        {
            _context.AccessRights.Remove(accessRightToDelete);
            _context.SaveChanges();
        }
    }
}
