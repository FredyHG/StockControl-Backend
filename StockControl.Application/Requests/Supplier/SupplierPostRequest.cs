using StockControl.Application.Requests.Address;
using StockControl.Application.Requests.Contact;

namespace StockControl.Application.Requests.Supplier;

public class SupplierPostRequest
{
    public string Name { get; set; }
    public string BusinessName { get; set; }
    public string CNPJ { get; set; }
    public List<ContactPostRequest>? Contacts { get; set; }
    public List<AddressPostRequest>? Addresses { get; set; }
}