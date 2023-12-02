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
        return _context.Supplies.ToList();
    }

    public Supply GetSupplyById(int supplyId)
    {
        return _context.Supplies.Find(supplyId);
    }

    // UPDATE
    public void UpdateSupply(Supply updatedSupply)
    {
        var existingSupply = _context.Supplies.Find(updatedSupply);

        if (existingSupply != null)
        {
            existingSupply.SupplierId = updatedSupply.SupplierId;
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
