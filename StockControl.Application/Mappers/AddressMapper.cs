using StockControl.Application.Requests.Address;
using StockControl.Domain.Entities;

namespace StockControl.Application.Mappers;

public class AddressMapper
{
    public static Address ToEntity(AddressPostRequest request, Supplier supplier)
    {
        return new Address(request.Street,
            request.Number,
            request.City,
            request.State,
            request.ZipCode,
            null,
            supplier.Id);
    }

    public static AddressGetRequest ToGetRequest(Address address, string supplierBusinessName)
    {
        return new AddressGetRequest(address.Id,
            address.Street,
            address.Number,
            address.City,
            address.State,
            address.ZipCode,
            supplierBusinessName);
    }
}