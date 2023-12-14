using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Application.Configurations;
using RabbitMQ.Application.Consumer;
using RabbitMQ.Application.Producer;
using RabbitMQ.Application.Queue;

namespace RabbitMQ.Infra.DependencyInjection;

public static class DependencyInjections
{
    public static void AddDependencyInjections(this IServiceCollection services, IConfigurationSection configuration)
    {
        services.Configure<RabbitMQConfiguration>(configuration);
        services.AddScoped<IQueueCreateUsecase, QueueCreateUsecase>();
        services.AddScoped<ISendMessageUsecase, SendMessageUsecase>();
        services.AddScoped<IReadMessageUsecase, ReadMessageUsecase>();
    }
}