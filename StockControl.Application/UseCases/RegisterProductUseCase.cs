using StockControl.Application.Mappers;
using StockControl.Application.Requests.Product;
using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class RegisterProductUseCase(IProductService productService, FindSupplierByCnpjUseCase findSupplierByCnpjUseCase)
{
    public async Task<Product> Execute(ProductPostRequest productPostRequest)
    {
        var supplier = await findSupplierByCnpjUseCase.Execute(productPostRequest.SupplierCnpj);

        var product = ProductMapper.ToEntity(productPostRequest, supplier);

        return await productService.RegisterProduct(product);
    }
}