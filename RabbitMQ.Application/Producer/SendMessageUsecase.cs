using Microsoft.Extensions.Options;
using RabbitMQ.Application.Configurations;
using RabbitMQ.Client;
using RabbitMQ.Models.Models;
using System;
using System.Text;

namespace RabbitMQ.Application.Producer;

public class SendMessageUsecase : ISendMessageUsecase
{
    private readonly RabbitMQConfiguration _configuration;

    public SendMessageUsecase(IOptions<RabbitMQConfiguration> configuration)
    {
        _configuration = configuration.Value;
        ArgumentNullException.ThrowIfNull(_configuration.RabbitMqUrl);
    }

    public void SendMessage(MessageInformation messageInformation)
    {
        var factory = new ConnectionFactory { HostName = _configuration.RabbitMqUrl };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        var properties = channel.CreateBasicProperties();
        properties.Persistent = false;
        byte[] messagebuffer = Encoding.Default.GetBytes(messageInformation.Message);
        string exchangeName = $"{messageInformation.QueueName}_exchange";
        string exchangeKey = $"{messageInformation.QueueName}_exchange_key";
        channel.BasicPublish(exchangeName, exchangeKey, properties, messagebuffer);
    }
}