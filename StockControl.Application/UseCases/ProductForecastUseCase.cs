using StockControl.Application.Mappers;
using StockControl.Application.Requests;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class ProductForecastUseCase(IProductService productService)
{
    public async Task<ProductForecastResponse> Execute(string skuCode, int months)
    {
        var productForeCastResponse = ProductMapper.dtoToResponse(await productService.ForecastBySku(skuCode, months));

        return productForeCastResponse;
    }
}