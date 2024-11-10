using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class CheckProductStockUseCase(IProductService productService)
{
    public async Task<bool> Execute(string productSKu, int quantity)
    {
        return await productService.CheckStock(productSKu, quantity);
    }
}