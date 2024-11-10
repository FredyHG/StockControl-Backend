using System.Text.Json.Serialization;

namespace StockControl.Domain.Entities;

public class Contact
{
    public Contact(string contactType,
        string contactName,
        string ddd,
        string number, Supplier supplier, string supplierId)
    {
        Supplier = supplier;
        SupplierId = supplierId;
        Id = Guid.NewGuid().ToString();
        ContactType = contactType;
        ContactName = contactName;
        Ddd = ddd;
        Number = number;
    }

    public Contact(string id,
        string contactType,
        string contactName,
        string ddd,
        string number)
    {
        Id = id;
        ContactType = contactType;
        ContactName = contactName;
        Ddd = ddd;
        Number = number;
    }

    public string Id { get; set; }
    public string ContactType { get; private set; }
    public string ContactName { get; private set; }
    public string Ddd { get; private set; }
    public string Number { get; private set; }

    [JsonIgnore] public Supplier Supplier { get; set; }

    public string SupplierId { get; set; }
}