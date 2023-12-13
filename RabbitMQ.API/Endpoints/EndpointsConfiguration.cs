using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using RabbitMQ.Application.Queue;
using System;

namespace RabbitMQ.API.Endpoints;

public static class EndpointsConfiguration
{
    public static void ConfigureEndpoints(this WebApplication app)
    {
        app.MapPost("/queues/create", (IQueueCreateUsecase queueCreateUsecase, string queueName) =>
        {
            try
            {
                queueCreateUsecase.CreateQueue(queueName);
                return Results.Ok($"Queue {queueName} created!");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }

        });
    }
}