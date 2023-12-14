using Microsoft.Extensions.Options;
using RabbitMQ.Application.Configurations;
using RabbitMQ.Client;
using System;

namespace RabbitMQ.Application.Queue;

public class QueueCreateUsecase : IQueueCreateUsecase
{
    private readonly RabbitMQConfiguration _configuration;

    public QueueCreateUsecase(IOptions<RabbitMQConfiguration> configuration)
    {
        _configuration = configuration.Value;
        ArgumentNullException.ThrowIfNull(_configuration.RabbitMqUrl);
    }

    public void CreateQueue(string queueName)
    {
        var factory = new ConnectionFactory { HostName = _configuration.RabbitMqUrl };
        using var rabbitConnection = factory.CreateConnection();
        using var channel = rabbitConnection.CreateModel();
        channel.QueueDeclare(queue: queueName,   // The name of queue
                             durable: false,     // Parameter to determine if queue will exists after broker breaks
                             exclusive: false,   // Determines if queue coexists with connection (case it closes, queue will be deleted)
                             autoDelete: false,  // Delete the queue in case no longer has consumers
                             arguments: null);   // General arguments
        string exchangeName = $"{queueName}_exchange";
        string exchangeKey = $"{queueName}_exchange_key";
        channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
        channel.QueueBind(queueName, exchangeName, exchangeKey);
    }
}