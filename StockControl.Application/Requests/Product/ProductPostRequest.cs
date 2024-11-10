namespace StockControl.Application.Requests.Product;

public class ProductPostRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal CostPrice { get; set; }
    public decimal PricePerUnit { get; set; }
    public int Stock { get; set; }
    public string ImageUrl { get; set; }
    public string SupplierCnpj { get; set; }

    public int CategoryId { get; set; }
}