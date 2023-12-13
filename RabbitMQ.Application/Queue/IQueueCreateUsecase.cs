namespace RabbitMQ.Application.Queue;

public interface IQueueCreateUsecase
{
    void CreateQueue(string queueName);
}