using StockControl.Application.Requests.Product;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class RegisterProductSellUseCase(IProductService productService)
{
    public async Task Execute(RegisterSellPostRequest request)
    {
        await productService.RegisterProductSellAsync(request.Products);
    }
}