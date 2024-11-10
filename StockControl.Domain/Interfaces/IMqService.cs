namespace StockControl.Domain.Interfaces;

public interface IMqService
{
    void SendMessage(string msg);
}