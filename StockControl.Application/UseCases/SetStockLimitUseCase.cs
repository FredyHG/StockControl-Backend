using StockControl.Application.Requests;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class SetStockLimitUseCase(IProductService productService)
{
    public async Task Execute(SetStockPost setStockPost)
    {
        await productService.SetProductStockLimit(setStockPost.productSku, setStockPost.minStock,
            setStockPost.maxStock);
    }
}