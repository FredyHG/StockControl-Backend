using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class FindSupplierByCnpjUseCase(ISupplierService supplierService)
{
    public Task<Supplier> Execute(string cnpj)
    {
        return supplierService.FindSupplierByCnpj(cnpj);
    }
}