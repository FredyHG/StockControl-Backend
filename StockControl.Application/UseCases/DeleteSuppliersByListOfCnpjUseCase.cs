using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class DeleteSuppliersByListOfCnpjUseCase(ISupplierService supplierService)
{
    public async Task Execute(List<string> cnpjs)
    {
        await supplierService.DeleteAllSuppliersByListOfCnpjs(cnpjs);
    }
}