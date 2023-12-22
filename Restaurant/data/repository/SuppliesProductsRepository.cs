using Microsoft.EntityFrameworkCore;
using Restaurant.app.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.data.repository;

public class SuppliesProductsRepository
{
    private readonly RestaurantDbContext _context;

    public SuppliesProductsRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void Add(SuppliesProducts suppliesProducts)
    {
        _context.SuppliesProducts.Add(suppliesProducts);
        _context.SaveChanges();
    }

    // READ
    public List<SuppliesProducts> Get()
    {

        return _context.SuppliesProducts.Include(p => p.Supply).
            Include(p => p.Product)
            .ToList();
    }


    // UPDATE
    public void Update(SuppliesProducts updatedSuppliesProducts)
    {
        var existingSuppliesProducts = _context.SuppliesProducts.Find(updatedSuppliesProducts.SupplyID, updatedSuppliesProducts.ProductID);

        if (existingSuppliesProducts != null)
        {
            existingSuppliesProducts.DeliveredQuantity = updatedSuppliesProducts.DeliveredQuantity;
            _context.SaveChanges();
        }
        else
        {
            _context.Entry(updatedSuppliesProducts).State = EntityState.Added;
            _context.SaveChanges();
        }
    }



    // DELETE
    public void Delete(int supplyId)
    {
        var suppliesProductsToDelete = _context.SuppliesProducts.Where(d => d.SupplyID == supplyId);

        if (suppliesProductsToDelete != null)
        {
            _context.SuppliesProducts.RemoveRange(suppliesProductsToDelete);
            _context.SaveChanges();
        }
    }
}
