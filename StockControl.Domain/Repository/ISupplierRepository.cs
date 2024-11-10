using StockControl.Domain.Entities;

namespace StockControl.Domain.Repository;

public interface ISupplierRepository : IBaseRepository<Supplier>
{
    Task DeleteByListOfCnpjs(List<string> cnpjs);
}