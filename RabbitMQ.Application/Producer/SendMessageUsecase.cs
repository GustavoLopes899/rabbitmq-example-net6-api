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
    private const string EXCHANGE_NAME = "exampleExchange";
    private const string EXCHANGE_KEY = "exampleExchangeKey";

    public SendMessageUsecase(IOptions<RabbitMQConfiguration> configuration)
    {
        _configuration = configuration.Value;
        ArgumentNullException.ThrowIfNull(_configuration.RabbitMqUrl);
    }

    public void SendMessage(MessageInformation messageInformation)
    {
        var factory = new ConnectionFactory { HostName = _configuration.RabbitMqUrl };
        var connection = factory.CreateConnection();
        var model = connection.CreateModel();
        var properties = model.CreateBasicProperties();
        properties.Persistent = false;
        byte[] messagebuffer = Encoding.Default.GetBytes(messageInformation.Message);
        model.ExchangeDeclare(EXCHANGE_NAME, ExchangeType.Direct);
        model.QueueBind(messageInformation.QueueName, EXCHANGE_NAME, EXCHANGE_KEY);
        model.BasicPublish(EXCHANGE_NAME, EXCHANGE_KEY, properties, messagebuffer);
    }
}