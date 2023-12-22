using Microsoft.EntityFrameworkCore;
using Restaurant.app.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.data.repository;

public class OrdersDishesRepository
{
    private readonly RestaurantDbContext _context;

    public OrdersDishesRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void Add(OrdersDishes ordersDishes)
    {
        _context.OrdersDishes.Add(ordersDishes);
        _context.SaveChanges();
    }

    // READ
    public List<OrdersDishes> Get()
    {

        return _context.OrdersDishes.Include(p => p.Dish).
            Include(p => p.Order)
            .ToList();
    }


    // UPDATE
    public void Update(OrdersDishes updatedOrdersDishes)
    {
        var existingOrdersDishes = _context.DishesProducts.Find(updatedOrdersDishes.DishID, updatedOrdersDishes.OrderID);

        if (existingOrdersDishes != null)
        {
            existingOrdersDishes.Quantity = updatedOrdersDishes.Quantity;
            _context.SaveChanges();
        }
        else
        {
            _context.Entry(updatedOrdersDishes).State = EntityState.Added;
            _context.SaveChanges();
        }
    }



    // DELETE
    public void Delete(int orderId)
    {
        var ordersDishesToDelete = _context.OrdersDishes.Where(d => d.OrderID == orderId);

        if (ordersDishesToDelete != null)
        {
            _context.OrdersDishes.RemoveRange(ordersDishesToDelete);
            _context.SaveChanges();
        }
    }
}
