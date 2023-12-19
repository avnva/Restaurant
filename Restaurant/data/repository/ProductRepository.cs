using System.Collections.Generic;
using System.Linq;

namespace Restaurant.repository;

public class ProductRepository
{
    private readonly RestaurantDbContext _context;

    public ProductRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    // CREATE
    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    // READ
    public List<Product> GetProducts()
    {
        return _context.Products.ToList();
    }

    public Product GetProductById(int productId)
    {
        return _context.Products.Find(productId);
    }

    // UPDATE
    public void UpdateProduct(Product updatedProduct)
    {
        var existingProduct = _context.Products.Find(updatedProduct);

        if (existingProduct != null)
        {
            existingProduct.ProductName = updatedProduct.ProductName;
            existingProduct.UnitsOfMeasureID = updatedProduct.UnitsOfMeasureID;
            existingProduct.PriceMarkup = updatedProduct.PriceMarkup;

            _context.SaveChanges();
        }
    }

    // DELETE
    public void DeleteProduct(int productId)
    {
        var productToDelete = _context.Products.Find(productId);

        if (productToDelete != null)
        {
            _context.Products.Remove(productToDelete);
            _context.SaveChanges();
        }
    }
}
