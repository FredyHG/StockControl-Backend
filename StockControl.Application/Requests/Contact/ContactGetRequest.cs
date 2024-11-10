namespace StockControl.Application.Requests.Contact;

public class ContactGetRequest
{
    public ContactGetRequest(string id, string contactType, string contactName, string ddd, string number, string supplierName)
    {
        Id = id;
        ContactType = contactType;
        ContactName = contactName;
        Ddd = ddd;
        Number = number;
        SupplierName = supplierName;
    }

    public string Id { get; set; }
    public string ContactType { get; set; }
    public string ContactName { get; set; }
    public string Ddd { get; set; }
    public string Number { get; set; }

    public string SupplierName { get; set; }
}