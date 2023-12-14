# rabbitmq-example-net6-api

An example from RabbitMQ API implementation. The parameters are stored in [appsettings.json](https://github.com/GustavoLopes899/rabbitmq-example-net6-api/blob/main/RabbitMQ.API/appsettings.json)

Basically we have four endpoints to manipulate the information in the queue:
1) /queue/create -> to create new queues;
2) /queue/delete -> to delete existing queues;
3) /message/send -> to send new messages to a specific queue;
4) /message/read ->to read and remove messages from a specific queue.

Note:
If necessary, there is a [docker-compose file](https://github.com/GustavoLopes899/rabbitmq-example-net6-api/blob/main/Docker-Files/RabbitMQ/docker-compose.yml) to instantiate a RabbitMQ instance
