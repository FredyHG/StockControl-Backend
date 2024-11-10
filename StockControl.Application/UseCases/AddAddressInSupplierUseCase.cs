using Microsoft.Extensions.Logging;
using StockControl.Application.Mappers;
using StockControl.Application.Requests.Address;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class AddAddressInSupplierUseCase(
    IAddressService addressService,
    ISupplierService supplierService,
    ILogger<AddAddressInSupplierUseCase> logger)
{
    public async Task Execute(AddressPostRequest request)
    {
        
        if(request.SupplierCnpj is null) throw new KeyNotFoundException("Supplier CNPJ is required");
        
        var supplier = await supplierService.FindSupplierByCnpj(request.SupplierCnpj);

        logger.LogInformation("Converting request to address");
        var address = AddressMapper.ToEntity(request, supplier);

        logger.LogInformation("Trying to add address");
        await addressService.AddAddress(supplier, address);
    }
}