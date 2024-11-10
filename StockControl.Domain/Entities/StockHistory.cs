namespace StockControl.Domain.Entities;

public class StockHistory(
    string transactionId,
    string productSku,
    string status,
    int actualStock,
    int oldStock,
    decimal totalPrice,
    decimal totalExpense,
    string transactionType,
    string category)
{
    public int Units = oldStock - actualStock;
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string TransactionId { get; set; } = transactionId;
    public string ProductSku { get; set; } = productSku;
    public string Status { get; set; } = status;
    public int ActualStock { get; set; } = actualStock;
    public int OldStock { get; set; } = oldStock;
    public decimal TotalPrice { get; set; } = totalPrice;
    public decimal TotalExpense { get; set; } = totalExpense;
    public string TransactionType { get; set; } = transactionType;
    public string Category { get; set; } = category;
    public DateTime TimeStamp { get; set; } = DateTime.Now;
}