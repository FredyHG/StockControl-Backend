using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;
using StockControl.Domain.Repository;

namespace StockControl.Domain.Services;

public class SupplierService(
    ISupplierRepository supplierRepository,
    IAddressRepository addressRepository,
    ILogger<SupplierService> logger) : ISupplierService
{
    public async Task<Supplier> RegisterSupplier(Supplier supplier)
    {
        logger.LogInformation("Starting RegisterSupplier method to register a new supplier.");

        var result = await supplierRepository.AddAsync(supplier);

        logger.LogInformation("Successfully registered supplier with ID: {SupplierId}", result.Id);

        return result;
    }

    public async Task<List<Supplier>> ListAllSupplier()
    {
        logger.LogInformation("Starting ListAllSupplier method to retrieve all suppliers.");

        var suppliers = await supplierRepository.GetAllAsync(query =>
            query.Include(sup => sup.Contacts)
                .Include(sup => sup.Addresses));

        logger.LogInformation("Successfully retrieved {SupplierCount} suppliers.", suppliers.Count);

        return suppliers;
    }

    public async Task<Supplier> FindSupplierByCnpj(string cnpj)
    {
        logger.LogInformation("Starting FindSupplierByCnpj method with CNPJ: {Cnpj}", cnpj);

        var supplier = await supplierRepository.FindByAsync(
            s => s.CNPJ == cnpj,
            query => query
                .Include(sup => sup.Contacts)
                .Include(sup => sup.Addresses));

        if (supplier is null)
        {
            logger.LogWarning("No supplier found with CNPJ: {Cnpj}. Throwing KeyNotFoundException.", cnpj);
            throw new KeyNotFoundException($"Supplier with CNPJ {cnpj} not found.");
        }

        logger.LogInformation("Supplier with CNPJ: {Cnpj} found successfully.", cnpj);

        return supplier;
    }
    
    public async Task DeleteAllSuppliersByListOfCnpjs(List<string> cnpjs)
    {
        logger.LogInformation("Starting DeleteAllSuppliersByListOfCnpjs for Cnpjs: {cnpjs}",
            string.Join(", ", cnpjs));

        await supplierRepository.DeleteByListOfCnpjs(cnpjs);

        logger.LogInformation("Successfully deleted suppliers with cnpjs: {cnpjs}", string.Join(", ", cnpjs));
    }
}