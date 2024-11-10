using StockControl.Domain.Dto;
using StockControl.Domain.Entities;

namespace StockControl.Domain.Interfaces;

public interface IProductService
{
    Task<Product> RegisterProduct(Product product);
    Task<List<Product>> ListAllProductsAsync();
    Task<List<Product>> FindAllProductsFiltered(string cpnj);
    Task DeleteProductBySkuCode(string skuCode);
    Task DeleteAllProductsByListOfSkuCodes(List<string> skuCodes);
    Task<Product> UpdateProduct(Product product);
    Task<Product> FindBySku(string sku);
    Task<bool> CheckStock(string productSku, int quantity);
    Task RegisterProductSellAsync(List<ProductSellDto> requestProducts);
    Task<Product> RestockProductAsync(RestockProductDto restockProductDto);
    Task SetProductStockLimit(string productSku, int minStock, int maxStock);
    Task<ProductForecastDto> ForecastBySku(string skuCode, int months);
}