using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class DeleteAddressByIdUseCase(IAddressService addressService)
{
    public async Task Execute(string id)
    {
        await addressService.DeleteAddressAsync(id);
    }
}