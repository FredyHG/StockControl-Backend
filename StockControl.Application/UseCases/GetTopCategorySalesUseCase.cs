using StockControl.Domain.Dto;
using StockControl.Domain.Interfaces;

namespace StockControl.Application.UseCases;

public class GetTopCategorySalesUseCase(IStockHistoryService stockHistoryService)
{
    public async Task<IEnumerable<SalesDataDto>> Execute()
    {
        return await stockHistoryService.GetTopCategorySalesData();
    }
}