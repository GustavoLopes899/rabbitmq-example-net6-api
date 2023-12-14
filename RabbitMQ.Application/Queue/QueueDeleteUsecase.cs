using Microsoft.Extensions.Options;
using RabbitMQ.Application.Configurations;
using RabbitMQ.Client;
using System;

namespace RabbitMQ.Application.Queue;

public class QueueDeleteUsecase : IQueueDeleteUsecase
{
    private readonly RabbitMQConfiguration _configuration;

    public QueueDeleteUsecase(IOptions<RabbitMQConfiguration> configuration)
    {
        _configuration = configuration.Value;
        ArgumentNullException.ThrowIfNull(_configuration.RabbitMqUrl);
    }

    public void DeleteQueue(string queueName)
    {
        var factory = new ConnectionFactory { HostName = _configuration.RabbitMqUrl };
        using var rabbitConnection = factory.CreateConnection();
        using var channel = rabbitConnection.CreateModel();
        channel.QueueDelete(queueName);
    }
}