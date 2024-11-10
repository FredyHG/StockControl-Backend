using StockControl.Application.Mappers;
using StockControl.Application.Requests.Supplier;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class ListAllSuppliersUseCase(ISupplierService supplierService)
{
    public async Task<List<SupplierGetRequest>> Execute()
    {
        var listAllSupplier = await supplierService.ListAllSupplier();

        return listAllSupplier
            .Select(SupplierMapper.ToGetRequest)
            .ToList();
    }
}