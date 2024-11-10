using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class DeleteProductsByListOfSkuCodesUseCase(IProductService productService)
{
    public async Task Execute(List<string> skuCodes)
    {
        await productService.DeleteAllProductsByListOfSkuCodes(skuCodes);
    }
}