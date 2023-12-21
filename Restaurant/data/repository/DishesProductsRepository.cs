using Restaurant.app.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.data.repository;

public class DishesProductsRepository
{
    private readonly RestaurantDbContext _context;

    public DishesProductsRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void Add(DishesProducts dishesProducts)
    {
        _context.DishesProducts.Add(dishesProducts);
        _context.SaveChanges();
    }

    // READ
    public List<DishesProducts> Get()
    {
        var dishProducts = _context.DishesProducts.ToList();

        foreach (var dish in _context.Dishes)
        {
            var ingredients = dishProducts.Where(dp => dp.DishID == dish.DishID).ToList();

            var distinctProducts = ingredients.GroupBy(dp => dp.ProductID)
                                             .Select(group => new
                                             {
                                                 ProductID = group.Key,
                                                 TotalQuantity = group.Sum(item => item.Quantity)
                                             })
                                             .ToList();

            // Удаляем текущие записи о продуктах для данного блюда
            _context.DishesProducts.RemoveRange(ingredients);

            // Добавляем новые записи с обновленными количествами продуктов
            foreach (var productGroup in distinctProducts)
            {
                _context.DishesProducts.Add(new DishesProducts
                {
                    //DishID = dish.DishID,
                    ProductID = productGroup.ProductID,
                    Quantity = productGroup.TotalQuantity
                });
            }
        }

        _context.SaveChanges();
        return _context.DishesProducts.ToList();
    }

    //public DishGroup GetDishGroupById(int groupId)
    //{
    //    return _context.DishGroups.Find(groupId);
    //}

    // UPDATE
    public void Update(DishesProducts updatedDishesProducts)
    {
        var existingDishesProducts = _context.DishesProducts.Find(updatedDishesProducts);

        if (existingDishesProducts != null)
        {
            existingDishesProducts.DishID = updatedDishesProducts.DishID;
            existingDishesProducts.ProductID = updatedDishesProducts.ProductID;
            existingDishesProducts.Quantity = updatedDishesProducts.Quantity;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void Delete(int groupId)
    {
        var dishesProductsToDelete = _context.DishesProducts.Find(groupId);

        if (dishesProductsToDelete != null)
        {
            _context.DishesProducts.Remove(dishesProductsToDelete);
            _context.SaveChanges();
        }
    }
}
