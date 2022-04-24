using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using EventBus.Messages;
using EventBus;

namespace Core.Users.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
    {
        private readonly ILogger logger;
        private readonly IEventBus eventBus;

        public AddUserCommandHandler(ILogger<AddUserCommandHandler> logger, IEventBus eventBus)
        {
            this.logger = logger;
            this.eventBus = eventBus;
        }
        public async Task<Unit> Handle(AddUserCommand request, CancellationToken ct = default)
        {
            logger.LogInformation("Получен запрос на создание нового пользователя.");

            await eventBus.Publish(new UserAddedEvent()
            {
                Name = request.Name,
                Surname = request.Surname,
                Patronymic = request.Patronymic,
                Phone = request.Phone,
                Email = request.Email
            }, Queues.USERS_QUEUE);

            logger.LogInformation("Сообщение о создании нового пользователя было успешно отправлено.");

            return await Unit.Task;
        }
    }
}
