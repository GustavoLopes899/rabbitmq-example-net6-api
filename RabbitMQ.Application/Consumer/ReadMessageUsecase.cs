using Microsoft.Extensions.Options;
using RabbitMQ.Application.Configurations;
using RabbitMQ.Application.Consumer;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Application.Producer;

public class ReadMessageUsecase : IReadMessageUsecase
{
    private readonly RabbitMQConfiguration _configuration;

    public ReadMessageUsecase(IOptions<RabbitMQConfiguration> configuration)
    {
        _configuration = configuration.Value;
        ArgumentNullException.ThrowIfNull(_configuration.RabbitMqUrl);
    }

    public List<string> ReadMessage(string queueName, int limit = 1)
    {
        var factory = new ConnectionFactory { HostName = _configuration.RabbitMqUrl };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        List<string> messageList = new List<string>();
        while (limit > 0 && channel.MessageCount(queueName) > 0)
        {
            var data = channel.BasicGet(queueName, true);
            if (data != null)
            {
                string message = Encoding.UTF8.GetString(data.Body.ToArray());
                messageList.Add(message);
            }

            limit--;
        }

        return messageList;
    }
}