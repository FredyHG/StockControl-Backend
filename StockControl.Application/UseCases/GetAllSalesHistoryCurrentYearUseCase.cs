using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class GetAllSalesHistoryCurrentYearUseCase(IStockHistoryService stockHistoryService)
{
    public async Task<Dictionary<string, int>> Execute()
    {
        return await stockHistoryService.GetAllSalesHistoryCurrentYear();
    }
}