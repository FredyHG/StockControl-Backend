using StockControl.Domain.Entities;

namespace StockControl.Domain.Interfaces;

public interface IAddressService
{
    Task DeleteAddressAsync(string id);
    Task AddAddress(Supplier supplier, Address address);
}