namespace StockControl.Domain.Entities;

public class Supplier
{
    public Supplier()
    {
    }

    public Supplier(string name, string businessName, string cnpj, List<Contact> contacts, List<Address> addresses)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        BusinessName = businessName;
        CNPJ = cnpj;
        Contacts = contacts;
        Addresses = addresses;
    }

    public string Id { get; set; }
    public string Name { get; private set; }

    public string BusinessName { get; private set; }

    public string CNPJ { get; private set; }

    public List<Contact> Contacts { get; private set; }

    public List<Address> Addresses { get; private set; }
}