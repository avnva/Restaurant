using System.Collections.Generic;
using System.Linq;
using Restaurant.app.model;

namespace Restaurant.repository;

public class DishRepository
{
    private readonly RestaurantDbContext _context;

    public DishRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void AddDish(Dish dish)
    {
        _context.Dishes.Add(dish);
        _context.SaveChanges();
    }

    // READ
    public List<Dish> GetDishes()
    {
        return _context.Dishes.ToList();
    }

    public Dish GetDishById(int dishId)
    {
        return _context.Dishes.Find(dishId);
    }

    // UPDATE
    public void UpdateDish(Dish updatedDish)
    {
        var existingDish = _context.Dishes.Find(updatedDish);

        if (existingDish != null)
        {
            existingDish.GroupId = updatedDish.GroupId;
            existingDish.DishName = updatedDish.DishName;
            existingDish.DishCost = updatedDish.DishCost;
            existingDish.OutputWeight = updatedDish.OutputWeight;
            existingDish.CookingTechnology = updatedDish.CookingTechnology;
            existingDish.Photo = updatedDish.Photo;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void DeleteDish(int dishId)
    {
        var dishToDelete = _context.Dishes.Find(dishId);

        if (dishToDelete != null)
        {
            _context.Dishes.Remove(dishToDelete);
            _context.SaveChanges();
        }
    }
}
