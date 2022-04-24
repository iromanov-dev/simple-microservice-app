using EventBus.Messages;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EventBus
{
    public class EventBus : IEventBus
    {
        private readonly IBus messageBus;

        public EventBus(IBus messageBus)
        {
            this.messageBus = messageBus;
        }

        public async Task Publish(IEvent @event, string queueName)
        {
            var endpoint = await messageBus.GetSendEndpoint(new Uri($"queue:{queueName}"));

            await endpoint.Send(@event);
        }
    }
}
