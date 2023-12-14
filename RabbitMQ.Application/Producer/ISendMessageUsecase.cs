using RabbitMQ.Models.Models;

namespace RabbitMQ.Application.Producer;

public interface ISendMessageUsecase
{
    void SendMessage(MessageInformation messageInformation);
}