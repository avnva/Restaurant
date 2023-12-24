using Microsoft.EntityFrameworkCore;
using Restaurant.app.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.data.repository;

public class RequestsProductsRepository
{
    private readonly RestaurantDbContext _context;

    public RequestsProductsRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void Add(RequestsProducts requestsProducts)
    {
        _context.RequestsProducts.Add(requestsProducts);
        _context.SaveChanges();
    }

    // READ
    public List<RequestsProducts> Get()
    {
        Console.WriteLine(_context.RequestsProducts.ToList());


        return _context.RequestsProducts.Include(p => p.Request).
            Include(p => p.Product)
            .ToList();

    }


    // UPDATE
    public void Update(RequestsProducts updatedRequestsProducts)
    {
        var existingRequestsProducts = _context.RequestsProducts.Find(updatedRequestsProducts.RequestID, updatedRequestsProducts.ProductID);

        if (existingRequestsProducts != null)
        {
            existingRequestsProducts.Quantity = updatedRequestsProducts.Quantity;
            _context.SaveChanges();
        }
        else
        {
            _context.Entry(updatedRequestsProducts).State = EntityState.Added;
            _context.SaveChanges();
        }
    }



    // DELETE
    public void Delete(int requestId)
    {
        var requestsProductsToDelete = _context.RequestsProducts.Where(d => d.RequestID == requestId);

        if (requestsProductsToDelete != null)
        {
            _context.RequestsProducts.RemoveRange(requestsProductsToDelete);
            _context.SaveChanges();
        }
    }
}
