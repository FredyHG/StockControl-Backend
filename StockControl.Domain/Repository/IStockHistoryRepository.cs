using StockControl.Domain.Dto;
using StockControl.Domain.Entities;

namespace StockControl.Domain.Repository;

public interface IStockHistoryRepository : IBaseRepository<StockHistory>
{
    Task<int> GetTotalUnitsAsync(int monthsAgo, string productSku);
    Task<Dictionary<string, int>> GetSalesPerMonth();
    Dictionary<string, int> GetSalesByMonthForCurrentYear();
    Task<IEnumerable<SalesDataDto>> GetTopCategorySalesData();
}