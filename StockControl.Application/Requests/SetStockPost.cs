namespace StockControl.Application.Requests;

public class SetStockPost
{
    public string productSku { get; set; }
    public int minStock { get; set; }
    public int maxStock { get; set; }
}