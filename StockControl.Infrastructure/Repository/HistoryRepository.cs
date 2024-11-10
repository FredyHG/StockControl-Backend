using System.Globalization;
using Microsoft.EntityFrameworkCore;
using StockControl.Domain.Dto;
using StockControl.Domain.Entities;
using StockControl.Domain.Repository;
using StockControl.Infrastructure.Data;

namespace StockControl.Infrastructure.Repository;

public class HistoryRepository(AppDbContext context) : BaseRepository<StockHistory>(context), IStockHistoryRepository
{
    private readonly AppDbContext _context = context;
    private readonly string SELL_MESSAGE = "Successfully";

    public async Task<int> GetTotalUnitsAsync(int monthsAgo, string productSku)
    {
        var startDate = DateTime.Now.AddMonths(-monthsAgo);

        var totalUnits = await _context.StockHistories
            .Where(sh => sh.Status == SELL_MESSAGE
                         && sh.TimeStamp >= startDate
                         && sh.ProductSku == productSku)
            .SumAsync(sh => sh.Units);

        return totalUnits;
    }

    public async Task<Dictionary<string, int>> GetSalesPerMonth()
    {
        var sales = await _context.StockHistories
            .Where(sh => sh.Status == SELL_MESSAGE)
            .GroupBy(sh => new { sh.TimeStamp.Year, sh.TimeStamp.Month })
            .Select(g => new
            {
                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.Month),
                TotalUnitsSold = g.Sum(sh => sh.Units)
            })
            .ToListAsync();

        return sales.ToDictionary(s => s.Month, s => s.TotalUnitsSold);
    }

    public Dictionary<string, int> GetSalesByMonthForCurrentYear()
    {
        var currentYear = DateTime.Now.Year;

        var salesByMonth = _context.StockHistories
            .Where(sh => sh.TimeStamp.Year == currentYear)
            .AsEnumerable()
            .GroupBy(sh => sh.TimeStamp.Month)
            .Select(g => new
            {
                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
                UnitsSum = g.Sum(sh => sh.Units)
            })
            .OrderBy(result => DateTime.ParseExact(result.Month, "MMMM", CultureInfo.CurrentCulture).Month)
            .ToDictionary(result => result.Month, result => result.UnitsSum);

        return salesByMonth;
    }

    public async Task<IEnumerable<SalesDataDto>> GetTopCategorySalesData()
    {
        var totalSalesValue = await _context.StockHistories
            .Where(s => s.Status == SELL_MESSAGE)
            .SumAsync(s => s.TotalPrice);

        var categorySales = await _context.StockHistories
            .Where(s => s.Status == SELL_MESSAGE)
            .GroupBy(s => s.Category)
            .Select(g => new SalesDataDto
            {
                Category = g.Key,
                SalesValue = g.Sum(s => s.TotalPrice),
                SalesPercentage = g.Sum(s => s.TotalPrice) / totalSalesValue * 100
            })
            .OrderByDescending(cs => cs.SalesPercentage)
            .Take(10)
            .ToListAsync();

        return categorySales;
    }
}