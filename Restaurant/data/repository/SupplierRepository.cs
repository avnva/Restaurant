using System.Collections.Generic;
using System.Linq;

namespace Restaurant.repository;

public class SupplierRepository
{
    private readonly RestaurantDbContext _context;

    public SupplierRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void AddSupplier(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        _context.SaveChanges();
    }

    // READ
    public List<Supplier> GetSuppliers()
    {
        return _context.Suppliers.ToList();
    }

    public Supplier GetSupplierById(int supplierId)
    {
        return _context.Suppliers.Find(supplierId);
    }

    // UPDATE
    public void UpdateSupplier(Supplier updatedSupplier)
    {
        var existingSupplier = _context.Suppliers.Find(updatedSupplier);

        if (existingSupplier != null)
        {
            existingSupplier.SupplierName = updatedSupplier.SupplierName;
            existingSupplier.Address = updatedSupplier.Address;
            existingSupplier.ContactPersonName = updatedSupplier.ContactPersonName;
            existingSupplier.Phone = updatedSupplier.Phone;
            existingSupplier.BankName = updatedSupplier.BankName;
            existingSupplier.BankAccount = updatedSupplier.BankAccount;
            existingSupplier.INN = updatedSupplier.INN;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void DeleteSupplier(int supplierId)
    {
        var supplierToDelete = _context.Suppliers.Find(supplierId);

        if (supplierToDelete != null)
        {
            _context.Suppliers.Remove(supplierToDelete);
            _context.SaveChanges();
        }
    }
}
