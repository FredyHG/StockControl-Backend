namespace StockControl.Domain.Entities;

public class Product
{
    public Product(string name,
        string description,
        decimal costPrice,
        decimal pricePerUnit,
        int stock,
        string imageUrl,
        Supplier supplier,
        int categoryId)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Description = description;
        CostPrice = costPrice;
        PricePerUnit = pricePerUnit;
        Stock = stock;
        SkuNumber = string.Empty;
        MinStock = 0;
        MaxStock = 0;
        ImageUrl = imageUrl;
        SupplierId = supplier.Id;
        CategoryId = categoryId;
    }

    public Product(string name,
        string skuNumber,
        string description,
        decimal costPrice,
        decimal pricePerUnit,
        int stock,
        int minStock,
        int maxStock,
        string imageUrl,
        Supplier supplier,
        int categoryId)
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
        Supplier = supplier;
        CategoryId = categoryId;
    }

    public Product(string id,
        string name,
        string skuNumber,
        string description,
        decimal costPrice,
        decimal pricePerUnit,
        int stock,
        int minStock,
        int maxStock,
        string imageUrl,
        Supplier supplier,
        int categoryId)
    {
        Id = Guid.Parse(id).ToString();
        Name = name;
        SkuNumber = skuNumber;
        Description = description;
        CostPrice = costPrice;
        PricePerUnit = pricePerUnit;
        Stock = stock;
        MinStock = minStock;
        MaxStock = maxStock;
        ImageUrl = imageUrl;
        Supplier = supplier;
        CategoryId = categoryId;
    }

    public Product(string name, string skuNumber, string description, decimal costPrice, decimal pricePerUnit)
    {
        Name = name;
        SkuNumber = skuNumber;
        Description = description;
        CostPrice = costPrice;
        PricePerUnit = pricePerUnit;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string SkuNumber { get; set; }
    public string Description { get; set; }
    public decimal CostPrice { get; set; }
    public decimal PricePerUnit { get; set; }
    public int Stock { get; set; }
    public int MinStock { get; set; }
    public int MaxStock { get; set; }
    public string ImageUrl { get; set; }
    public Supplier Supplier { get; set; }
    public Category Category { get; set; }

    public string SupplierId { get; set; }
    public int? CategoryId { get; set; }
}