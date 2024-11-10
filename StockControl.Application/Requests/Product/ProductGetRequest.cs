using StockControl.Application.Requests.Supplier;
using StockControl.Domain.Entities;

namespace StockControl.Application.Requests.Product;

public class ProductGetRequest
{
    public ProductGetRequest(string name, string skuNumber, string description, decimal costPrice, decimal pricePerUnit,
        int stock, int minStock, int maxStock, string imageUrl, Category category, SupplierGetRequest supplier)
    {
        Name = name;
        SkuNumber = skuNumber;
        Description = description;
        CostPrice = costPrice;
        PricePerUnit = pricePerUnit;
        Stock = stock;
        MinStock = minStock;
        MaxStock = maxStock;
        ImageUrl = imageUrl;
        Category = category;
        Supplier = supplier;
    }

    public string Name { get; private set; }
    public string SkuNumber { get; private set; }
    public string Description { get; private set; }
    public decimal CostPrice { get; private set; }
    public decimal PricePerUnit { get; private set; }
    public int Stock { get; private set; }
    public int MinStock { get; private set; }
    public int MaxStock { get; private set; }
    public string ImageUrl { get; private set; }
    public Category Category { get; set; }
    public SupplierGetRequest Supplier { get; set; }
}