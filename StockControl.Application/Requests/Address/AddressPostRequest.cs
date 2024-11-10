namespace StockControl.Application.Requests.Address;

public class AddressPostRequest
{
    public string Street { get; set; }
    public int Number { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    public string? SupplierCnpj { get; set; }
}