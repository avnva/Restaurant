using System.Collections.Generic;
using System.Linq;

namespace Restaurant.repository;

public class MenuRepository
{
    private readonly RestaurantDbContext _context;

    public MenuRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void AddDishToMenu(Menu menu)
    {
        _context.Menu.Add(menu);
        _context.SaveChanges();
    }

    // READ
    public List<Menu> GetMenu()
    {
        return _context.Menu.ToList();
    }

    public Menu GetDishInMenuById(int dishInMenuId)
    {
        return _context.Menu.Find(dishInMenuId);
    }
    public int GetMaxDishId()
    {
        // Получаем максимальный ID из базы данных
        var maxId = _context.Menu.Max(d => d.DishInMenuID) ?? 0;
        return maxId;
    }
    // UPDATE
    public void UpdateDishInMenu(Menu updatedMenu)
    {
        var existingMenu = _context.Menu.Find(updatedMenu.DishInMenuID);

        if (existingMenu != null)
        {
            existingMenu.DishId = updatedMenu.DishId;
            existingMenu.StatusId = updatedMenu.StatusId;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void RemoveDishFromMenu(int? dishInMenuId)
    {
        var menuToRemove = _context.Menu.Find(dishInMenuId);

        if (menuToRemove != null)
        {
            _context.Menu.Remove(menuToRemove);
            _context.SaveChanges();
        }
    }
}
