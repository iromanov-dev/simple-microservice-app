using Core.Organizations.Commands.AddUser;
using EventBus.Messages;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Core.Organizations.Events.Subscribers.UserAdded
{
    public class UserAddedSubscriber : IConsumer<UserAddedEvent>
    {
        private readonly IMediator mediator;
        private readonly ILogger logger;

        public UserAddedSubscriber() { }

        public UserAddedSubscriber(IMediator mediator, ILogger<UserAddedSubscriber> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<UserAddedEvent> context)
        {
            logger.LogInformation($"Получено сообщение {nameof(UserAddedEvent)}");

            await mediator.Send(new AddUserCommand()
            {
                Name = context.Message.Name,
                Surname = context.Message.Surname,
                Patronymic = context.Message.Patronymic,
                Phone = context.Message.Phone,
                Email = context.Message.Email
            });
        }
    }
}
