namespace StockControl.Application.Requests.Address;

public class AddressGetRequest(
    string id,
    string street,
    int number,
    string city,
    string state,
    string zipCode,
    string supplierName)
{
    public string Id { get; set; } = id;
    public string Street { get; set; } = street;
    public int Number { get; set; } = number;
    public string City { get; set; } = city;
    public string State { get; set; } = state;
    public string ZipCode { get; set; } = zipCode;

    public string SupplierName { get; set; } = supplierName;
}