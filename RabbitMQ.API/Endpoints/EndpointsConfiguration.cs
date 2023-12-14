using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using RabbitMQ.Application.Producer;
using RabbitMQ.Application.Queue;
using RabbitMQ.Models.Models;
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

        app.MapPost("/message/send", (ISendMessageUsecase sendMessageUsecase, MessageInformation messageInformation) =>
        {
            try
            {
                sendMessageUsecase.SendMessage(messageInformation);
                return Results.Ok($"Message was sended to queue {messageInformation.QueueName}!");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
    }
}