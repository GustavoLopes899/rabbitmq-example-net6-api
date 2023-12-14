namespace RabbitMQ.Application.Queue;

public interface IQueueDeleteUsecase
{
    void DeleteQueue(string queueName);
}