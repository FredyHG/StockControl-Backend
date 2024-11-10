namespace StockControl.Application.Requests.Contact;

public class ContactPostRequest
{
    public string ContactType { get; set; }
    public string ContactName { get; set; }
    public string Ddd { get; set; }
    public string Number { get; set; }

    public string? SupplierCnpj { get; set; }
}