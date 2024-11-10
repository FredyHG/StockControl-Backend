namespace StockControl.Domain.Dto;

public class NotificationDto(
    string type,
    string productName,
    int productQuantity,
    int minStock,
    int maxStock,
    string recipient)
{
    public string Type { get; set; } = type;
    public string ProductName { get; set; } = productName;
    public int ProductQuantity { get; set; } = productQuantity;
    public int MinStock { get; set; } = minStock;
    public int MaxStock { get; set; } = maxStock;
    public string Recipient { get; set; } = recipient;
}