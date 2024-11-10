using StockControl.Application.Requests.Product;
using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class UpdateProductInfosUseCase(IProductService productService)
{
    public async Task<Product> Execute(ProductPutRequest productPutRequest)
    {
        var productTarget = await productService.FindBySku(productPutRequest.SkuNumber);

        ObjectMapper.CopyNonNull(productPutRequest, productTarget);

        return await productService.UpdateProduct(productTarget);
    }
}