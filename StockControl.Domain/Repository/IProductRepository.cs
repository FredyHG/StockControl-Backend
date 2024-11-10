using StockControl.Domain.Entities;

namespace StockControl.Domain.Repository;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<List<Product>> FindAllBySupplierCnpj(string cnpj);
    Task DeleteBySkuCode(string skuCode);
    Task DeleteByListOfSkuCode(List<string> skusCodes);
}