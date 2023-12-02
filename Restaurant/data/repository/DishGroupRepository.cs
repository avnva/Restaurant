using System.Collections.Generic;
using System.Linq;

namespace Restaurant.repository;

public class DishGroupRepository
{
    private readonly RestaurantDbContext _context;

    public DishGroupRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void AddDishGroup(DishGroup dishGroup)
    {
        _context.DishesGroups.Add(dishGroup);
        _context.SaveChanges();
    }

    // READ
    public List<DishGroup> GetDishGroups()
    {
        return _context.DishesGroups.ToList();
    }

    public DishGroup GetDishGroupById(int groupId)
    {
        return _context.DishesGroups.Find(groupId);
    }

    // UPDATE
    public void UpdateDishGroup(DishGroup updatedDishGroup)
    {
        var existingDishGroup = _context.DishesGroups.Find(updatedDishGroup);

        if (existingDishGroup != null)
        {
            existingDishGroup.GroupName = updatedDishGroup.GroupName;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void DeleteDishGroup(int groupId)
    {
        var dishGroupToDelete = _context.DishesGroups.Find(groupId);

        if (dishGroupToDelete != null)
        {
            _context.DishesGroups.Remove(dishGroupToDelete);
            _context.SaveChanges();
        }
    }
}
