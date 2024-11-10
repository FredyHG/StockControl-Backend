using Microsoft.Extensions.Logging;
using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;
using StockControl.Domain.Repository;

namespace StockControl.Domain.Services;

public class AddressService(IAddressRepository addressRepository, ILogger<AddressService> logger) : IAddressService
{
    public async Task DeleteAddressAsync(string id)
    {
        Console.WriteLine($"Deleting address: {id}");

        var address = await FindAddressById(id);

        await addressRepository.DeleteAsync(address);
    }

    public async Task AddAddress(Supplier supplier, Address address)
    {
        logger.LogInformation("Starting AddAddress method for supplier ID {SupplierId}", supplier.Id);

        if (supplier.Addresses.Count >= 3)
        {
            logger.LogWarning("Supplier ID {SupplierId} already has three addresses. Throwing exception.", supplier.Id);
            throw new ArgumentException("Supplier already contains three addresses");
        }

        await addressRepository.AddAsync(address);

        logger.LogInformation("Address successfully added to supplier ID {SupplierId}", supplier.Id);
    }

    public async Task<Address> FindAddressById(string id)
    {
        var address = await addressRepository.FindByAsync(a => a.Id == id);

        if (address is null)
            throw new KeyNotFoundException();

        return address;
    }
}