using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.repository;

public class OrderRepository
{
    private readonly RestaurantDbContext _context;

    public OrderRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void AddOrder(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    // READ
    public List<Order> GetOrders()
    {
        return _context.Orders.ToList();
        //return _context.Orders.Include(o => o.OrderedDishes).ToList();
    }
    public int GetMaxOrderId()
    {
        // Получаем максимальный ID из базы данных
        var maxId = _context.Orders.Max(d => (int?)d.OrderID) ?? 0;
        return maxId;
    }
    public Order GetOrderById(int orderId)
    {
        return _context.Orders.Include(o => o.OrderedDishes).FirstOrDefault(o => o.OrderID == orderId);
    }

    // UPDATE
    public void UpdateOrder(Order updatedOrder)
    {
        //var existingOrder = _context.Orders.Include(o => o.OrderedDishes).FirstOrDefault(o => o.OrderID == updatedOrder.OrderID);
        var existingOrder = _context.Orders.Find(updatedOrder.OrderID);
        if (existingOrder != null)
        {
            existingOrder.OrderDate = updatedOrder.OrderDate;
            existingOrder.OrderCost = updatedOrder.OrderCost;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void DeleteOrder(int orderId)
    {
        var orderToDelete = _context.Orders.Find(orderId);

        if (orderToDelete != null)
        {
            _context.Orders.Remove(orderToDelete);
            _context.SaveChanges();
        }
    }
}
