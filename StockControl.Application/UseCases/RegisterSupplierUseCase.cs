using StockControl.Application.Mappers;
using StockControl.Application.Requests.Supplier;
using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class RegisterSupplierUseCase(ISupplierService supplierService)
{
    public async Task<Supplier> Execute(SupplierPostRequest request)
    {
        var supplier = SupplierMapper.ToEntity(request);
        return await supplierService.RegisterSupplier(supplier);
    }
}