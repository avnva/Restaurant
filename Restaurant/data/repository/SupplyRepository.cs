using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.repository;

public class SupplyRepository
{
    private readonly RestaurantDbContext _context;

    public SupplyRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void AddSupply(Supply supply)
    {
        _context.Supplies.Add(supply);
        _context.SaveChanges();
    }

    // READ
    public List<Supply> GetSupplies()
    {
        return _context.Supplies
                .Include(w => w.Supplier)
                .ToList();
    }

    public Supply GetSupplyById(int supplyId)
    {
        return _context.Supplies.Find(supplyId);
    }
    public int GetMaxSupplyId()
    {
        // Получаем максимальный ID из базы данных
        var maxId = _context.Supplies.Max(d => (int?)d.SupplyID) ?? 0;
        return maxId;
    }

    // UPDATE
    public void UpdateSupply(Supply updatedSupply)
    {
        var existingSupply = _context.Supplies.Find(updatedSupply.SupplyID);

        if (existingSupply != null)
        {
            existingSupply.SupplierID = updatedSupply.SupplierID;
            existingSupply.SupplyDate = updatedSupply.SupplyDate;
            existingSupply.PurchasePrice = updatedSupply.PurchasePrice;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void DeleteSupply(int supplyId)
    {
        var supplyToDelete = _context.Supplies.Find(supplyId);

        if (supplyToDelete != null)
        {
            _context.Supplies.Remove(supplyToDelete);
            _context.SaveChanges();
        }
    }
}
