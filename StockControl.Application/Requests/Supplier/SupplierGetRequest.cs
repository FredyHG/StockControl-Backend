using StockControl.Application.Requests.Address;
using StockControl.Application.Requests.Contact;

namespace StockControl.Application.Requests.Supplier;

public class SupplierGetRequest
{
    public SupplierGetRequest(string name, string businessName, string cnpj, List<ContactGetRequest> contacts,
        List<AddressGetRequest> addresses)
    {
        Name = name;
        BusinessName = businessName;
        CNPJ = cnpj;
        Contacts = contacts;
        Addresses = addresses;
    }

    public SupplierGetRequest(string name, string businessName, string cnpj, List<ContactGetRequest> contacts,
        List<AddressGetRequest> address, List<AddressGetRequest> addresses)
    {
        Name = name;
        BusinessName = businessName;
        CNPJ = cnpj;
        Contacts = contacts;
        Addresses = address;
    }

    public string Name { get; set; }
    public string BusinessName { get; set; }
    public string CNPJ { get; set; }
    public List<ContactGetRequest> Contacts { get; set; }
    public List<AddressGetRequest> Addresses { get; set; }
}