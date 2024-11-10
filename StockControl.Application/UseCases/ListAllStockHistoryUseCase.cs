using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class ListAllStockHistoryUseCase(IStockHistoryService stockHistoryService)
{
    public async Task<IEnumerable<StockHistory>> Execute()
    {
        return await stockHistoryService.ListAllStockHistoryAsync();
    }
}