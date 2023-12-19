using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.repository;

public class WarehouseRepository
{
    private readonly RestaurantDbContext _context;

    public WarehouseRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void AddWarehouse(Warehouse warehouse)
    {
        _context.Warehouses.Add(warehouse);
        _context.SaveChanges();
    }

    // READ
    public List<Warehouse> GetWarehouses()
    {
        return _context.Warehouses
    .Include(w => w.Product)  // Загрузка связанной сущности Product
    .Include(w => w.Supplier) // Загрузка связанной сущности Supplier
    .ToList();
    }

    public Warehouse GetWarehouseById(int warehouseId)
    {
        return _context.Warehouses.Find(warehouseId);
    }

    // UPDATE
    public void UpdateWarehouse(Warehouse updatedWarehouse)
    {
        var existingWarehouse = _context.Warehouses.Find(updatedWarehouse);

        if (existingWarehouse != null)
        {
            existingWarehouse.ProductID = updatedWarehouse.ProductID;
            existingWarehouse.StockBalance = updatedWarehouse.StockBalance;
            existingWarehouse.SupplierID = updatedWarehouse.SupplierID;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void DeleteWarehouse(int warehouseId)
    {
        var warehouseToDelete = _context.Warehouses.Find(warehouseId);

        if (warehouseToDelete != null)
        {
            _context.Warehouses.Remove(warehouseToDelete);
            _context.SaveChanges();
        }
    }
}
