using Microsoft.AspNetCore.Mvc;
using StockControl.Application.UseCases;
using StockControl.Domain.Dto;
using StockControl.Domain.Entities;

namespace StockControl.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockHistoryController(ILogger<StockHistoryController> logger)
{
    [HttpGet("current-year")]
    public async Task<Dictionary<string, int>> GetAllSalesHistoryLastYear(
        [FromServices] GetAllSalesHistoryCurrentYearUseCase useCase)
    {
        logger.LogInformation("Receive request to get all sales history of last year");

        return await useCase.Execute();
    }

    [HttpGet("top-category")]
    public async Task<IEnumerable<SalesDataDto>> GetTopCategorySales([FromServices] GetTopCategorySalesUseCase useCase)
    {
        logger.LogInformation("Receive request to get sales per month");

        return await useCase.Execute();
    }

    [HttpGet("list-all")]
    public async Task<IEnumerable<StockHistory>> GetAllStockHistory([FromServices] ListAllStockHistoryUseCase useCase)
    {
        logger.LogInformation("Receive request to get all stock history");

        return await useCase.Execute();
    }
}