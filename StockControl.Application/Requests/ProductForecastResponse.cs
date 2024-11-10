namespace StockControl.Application.Requests;

public class ProductForecastResponse
{
    public ProductForecastResponse(string skuCode, decimal forecastedStockNextMonths,
        Dictionary<string, int> salesPerMonth)
    {
        SkuCode = skuCode;
        ForecastedStockNextMonths = forecastedStockNextMonths;
        SalesPerMonth = salesPerMonth;
    }

    public string SkuCode { get; set; }
    public decimal ForecastedStockNextMonths { get; set; }

    public Dictionary<string, int> SalesPerMonth { get; set; }
}