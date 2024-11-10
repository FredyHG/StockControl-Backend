using Microsoft.EntityFrameworkCore;
using StockControl.Domain.Entities;
using StockControl.Domain.Repository;
using StockControl.Infrastructure.Data;

namespace StockControl.Infrastructure.Repository;

public class SupplierRepository(AppDbContext context) : BaseRepository<Supplier>(context), ISupplierRepository
{
    public async Task DeleteByListOfCnpjs(List<string> cnpjs)
    {
        var suppliers = await context.Suppliers
            .Where(p => cnpjs.Contains(p.CNPJ))
            .ToListAsync();

        if (suppliers == null || !suppliers.Any())
            throw new KeyNotFoundException("No suppliers found with the provided cnpjs.");

        context.Suppliers.RemoveRange(suppliers);

        await context.SaveChangesAsync();
    }
}