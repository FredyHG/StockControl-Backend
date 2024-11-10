using StockControl.Application.Mappers;
using StockControl.Application.Requests.Product;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class ListAllProductsUseCase(IProductService productService)
{
    public async Task<List<ProductGetRequest>> Execute()
    {
        var listAllProducts = await productService.ListAllProductsAsync();

        return listAllProducts
            .Select(ProductMapper.ToGetRequest)
            .ToList();
    }
}