using System.Text;
using RabbitMQ.Client;
using StockControl.Domain.Interfaces;

namespace StockControl.Infrastructure.Services;

public class MqService(ConnectionFactory factory, string exchangeName, string queueName)
    : IMqService
{
    public void SendMessage(string msg)
    {
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
        channel.QueueDeclare(queueName, false, false, false, null);
        channel.QueueBind(queueName, exchangeName, "");

        var body = Encoding.UTF8.GetBytes(msg);
        channel.BasicPublish(exchangeName, "", null, body);
    }
}