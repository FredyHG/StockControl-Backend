using StockControl.Domain.Dto;
using StockControl.Domain.Entities;

namespace StockControl.Domain.Interfaces;

public interface IStockHistoryService
{
    Task<IEnumerable<SalesDataDto>> GetTopCategorySalesData();
    Task<IEnumerable<StockHistory>> ListAllStockHistoryAsync();
    Task<Dictionary<string, int>> GetAllSalesHistoryCurrentYear();
}