using StockControl.Domain.Dto;
using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;
using StockControl.Domain.Repository;

namespace StockControl.Domain.Services;

public class StockHistoryService(IStockHistoryRepository stockHistoryRepository) : IStockHistoryService
{
    public async Task<IEnumerable<SalesDataDto>> GetTopCategorySalesData()
    {
        return await stockHistoryRepository.GetTopCategorySalesData();
    }

    public async Task<IEnumerable<StockHistory>> ListAllStockHistoryAsync()
    {
        return await stockHistoryRepository.GetAllAsync();
    }

    public async Task<Dictionary<string, int>> GetAllSalesHistoryCurrentYear()
    {
        return await Task.Run(stockHistoryRepository.GetSalesByMonthForCurrentYear);
    }
}