namespace StockControl.Domain.Dto;

public class ProductForecastDto(
    string skuCode,
    decimal forecastedStockNextMonths,
    Dictionary<string, int> salesPerMonth)
{
    public string SkuCode { get; set; } = skuCode;
    public decimal ForecastedStockNextMonths { get; set; } = forecastedStockNextMonths;
    public Dictionary<string, int> SalesPerMonth { get; set; } = salesPerMonth;
}