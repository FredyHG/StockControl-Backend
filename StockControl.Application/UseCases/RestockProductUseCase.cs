using StockControl.Domain.Dto;
using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class RestockProductUseCase(IProductService productService)
{
    public async Task<Product> Execute(RestockProductDto request)
    {
        return await productService.RestockProductAsync(request);
    }
}