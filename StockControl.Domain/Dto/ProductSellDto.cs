namespace StockControl.Domain.Dto;

public class ProductSellDto(string productSku, int quantity)
{
    public string ProductSku { get; set; } = productSku;
    public int Quantity { get; set; } = quantity;
}