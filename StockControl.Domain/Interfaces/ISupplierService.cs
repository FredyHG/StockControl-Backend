using StockControl.Domain.Entities;

namespace StockControl.Domain.Interfaces;

public interface ISupplierService
{
    Task<Supplier> RegisterSupplier(Supplier supplier);
    Task<List<Supplier>> ListAllSupplier();
    Task<Supplier> FindSupplierByCnpj(string cnpj);
    Task DeleteAllSuppliersByListOfCnpjs(List<string> cnpjs);
}