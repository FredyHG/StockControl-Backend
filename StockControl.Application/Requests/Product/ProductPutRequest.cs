namespace StockControl.Application.Requests.Product;

public class ProductPutRequest
{
    public string Name { get; set; }
    public string SkuNumber { get; set; }
    public string Description { get; set; }
    public decimal CostPrice { get; set; }
    public decimal PricePerUnit { get; set; }
}