using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class DeleteProductBySkuCodeUseCase(IProductService productService)
{
    public async Task Execute(string id)
    {
        await productService.DeleteProductBySkuCode(id);
    }
}