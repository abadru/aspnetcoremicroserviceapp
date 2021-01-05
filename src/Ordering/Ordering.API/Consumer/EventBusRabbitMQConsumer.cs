using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Common;
using MediatR;
using Ordering.Core.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Consumer
{
    public class EventBusRabbitMQConsumer
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repository;

        public EventBusRabbitMQConsumer(IRabbitMQConnection connection, IMediator mediator, IMapper mapper, IOrderRepository repository)
        {
            _connection = connection;
            _mediator = mediator;
            _mapper = mapper;
            _repository = repository;
        }

        public void Consume()
        {
            var channel = _connection.CreateModel();

            channel.QueueDeclare(queue: EventBusConstants.BasketCheckoutQueue, durable: false,
                exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.BasketCheckoutQueue, autoAck: true, consumer: consumer);
        }
    }
}
