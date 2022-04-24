using EventBus.Messages;
using System.Threading.Tasks;

namespace EventBus
{
    public interface IEventBus
    {
        public Task Publish<T>(T @event, string queueName);
    }
}
