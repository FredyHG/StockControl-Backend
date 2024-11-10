using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class FindAllProductsBySupplierCnpjUseCase(IProductService productService)
{
    public async Task<List<Product>> Execute(string cnpj)
    {
        return await productService.FindAllProductsFiltered(cnpj);
    }
}