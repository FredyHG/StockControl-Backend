using StockControl.Application.Requests.Address;
using StockControl.Application.Requests.Contact;
using StockControl.Application.Requests.Supplier;
using StockControl.Domain.Entities;

namespace StockControl.Application.Mappers;

public abstract class SupplierMapper
{
    public static Supplier ToEntity(SupplierPostRequest request)
    {
        List<Contact> contacts = [];
        List<Address> addresses = [];

        var supplier = new Supplier(request.Name, request.BusinessName, request.CNPJ, [], []);

        if (addresses.Count > 0 || addresses.Count > 0)
        {
            addresses.AddRange(request.Addresses.Select(addressPostRequest =>
                AddressMapper.ToEntity(addressPostRequest, supplier)));
            contacts.AddRange(request.Contacts.Select(contactPostRequest =>
                ContactMapper.ToEntity(contactPostRequest, supplier)));

            supplier.Contacts.AddRange(contacts);
            supplier.Addresses.AddRange(addresses);
        }

        return supplier;
    }

    public static SupplierGetRequest ToGetRequest(Supplier supplier)
    {
        List<ContactGetRequest> contactsGet = [];
        List<AddressGetRequest> addressesGet = [];

        var supplierGet = new SupplierGetRequest(supplier.Name, supplier.BusinessName, supplier.CNPJ, [], []);

        addressesGet.AddRange(supplier.Addresses.Select(addressPostRequest =>
            AddressMapper.ToGetRequest(addressPostRequest, supplier.BusinessName)));
        contactsGet.AddRange(supplier.Contacts.Select(contactPostRequest =>
            ContactMapper.ToGetRequest(contactPostRequest, supplier.BusinessName)));

        supplierGet.Contacts.AddRange(contactsGet);
        supplierGet.Addresses.AddRange(addressesGet);

        return supplierGet;
    }
}