﻿namespace StockControl.Infrastructure.Configurations;

public class MqSettings
{
    public string HostName { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string ExchangeName { get; set; }
    public string QueueName { get; set; }
}