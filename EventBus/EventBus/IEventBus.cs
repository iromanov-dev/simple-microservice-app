using EventBus.Messages;
using System;
using System.Threading.Tasks;

namespace EventBus
{
    public interface IEventBus
    {
        public Task Publish(IEvent @event, string queueName);
    }
}
