using System.Text.Json.Serialization;

namespace StockControl.Domain.Entities;

public class Address
{
    public Address(string street,
        int number,
        string city,
        string state,
        string zipCode, Supplier supplier, string supplierId)
    {
        Supplier = supplier;
        SupplierId = supplierId;
        Id = Guid.NewGuid().ToString();
        Street = street;
        Number = number;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    public Address(string id,
        string street,
        int number,
        string city,
        string state,
        string zipCode)
    {
        Id = Guid.Parse(id).ToString();
        Street = street;
        Number = number;
        City = city;
        State = state;
        ZipCode = zipCode;
    }


    public string Id { get; set; }
    public string Street { get; private set; }
    public int Number { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }

    [JsonIgnore] public Supplier Supplier { get; set; }

    public string SupplierId { get; set; }
}