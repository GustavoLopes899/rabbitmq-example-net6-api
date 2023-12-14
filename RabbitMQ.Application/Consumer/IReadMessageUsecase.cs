using System.Collections.Generic;

namespace RabbitMQ.Application.Consumer;

public interface IReadMessageUsecase
{
    List<string> ReadMessage(string queueName, int limit = 1);
}