using Microsoft.EntityFrameworkCore;
using StockControl.Domain.Entities;
using StockControl.Domain.Repository;
using StockControl.Infrastructure.Data;

namespace StockControl.Infrastructure.Repository;

public class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
{
    private readonly AppDbContext _context = context;

    public async Task<List<Product>> FindAllBySupplierCnpj(string cnpj)
    {
        return await _context.Products
            .Where(p => p.Supplier.CNPJ == cnpj)
            .Include(p => p.Supplier)
            .ThenInclude(s => s.Contacts)
            .Include(p => p.Supplier)
            .ThenInclude(s => s.Addresses)
            .Include(cat => cat.Category)
            .ToListAsync();
    }

    public async Task DeleteBySkuCode(string skuCode)
    {
        var product = await _context.Products.FirstOrDefaultAsync(s => s.SkuNumber == skuCode);

        if (product == null)
            throw new KeyNotFoundException("Product not found.");

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByListOfSkuCode(List<string> skusCodes)
    {
        var products = await _context.Products
            .Where(p => skusCodes.Contains(p.SkuNumber))
            .ToListAsync();

        if (products == null || !products.Any())
            throw new KeyNotFoundException("No products found with the provided SKU codes.");

        _context.Products.RemoveRange(products);

        await _context.SaveChangesAsync();
    }
}