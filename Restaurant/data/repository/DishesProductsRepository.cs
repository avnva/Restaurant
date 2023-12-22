using Microsoft.EntityFrameworkCore;
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

        Console.WriteLine(context.DishesProducts.First());
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
        Console.WriteLine(_context.DishesProducts.ToList());


        return _context.DishesProducts.Include(p => p.Dish).
            Include(p => p.Product)
            .ToList();

    }


    // UPDATE
    public void Update(DishesProducts updatedDishesProducts)
    {
        var existingDishesProducts = _context.DishesProducts.Find(updatedDishesProducts.DishID, updatedDishesProducts.ProductID);

        if (existingDishesProducts != null)
        {
            existingDishesProducts.Quantity = updatedDishesProducts.Quantity;
            _context.SaveChanges();
        }
        else
        {
            _context.Entry(updatedDishesProducts).State = EntityState.Added;
            _context.SaveChanges();
        }
    }



    // DELETE
    public void Delete(int dishId)
    {
        var dishesProductsToDelete = _context.DishesProducts.Where(d => d.DishID == dishId);

        if (dishesProductsToDelete != null)
        {
            _context.DishesProducts.RemoveRange(dishesProductsToDelete);
            _context.SaveChanges();
        }
    }
}
