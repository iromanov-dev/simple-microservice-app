using Core.Users.Events.Publish.UserAddedEvent;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Users.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand>
    {
        private readonly ILogger logger;
        private readonly IBus messageBus;

        public AddUserCommandHandler(ILogger<AddUserCommandHandler> logger, IBus messageBus)
        {
            this.logger = logger;
            this.messageBus = messageBus;
        }
        public async Task<Unit> Handle(AddUserCommand request, CancellationToken ct = default)
        {
            logger.LogInformation("Получен запрос на создание нового пользователя.");
            var endpoint = await messageBus.GetSendEndpoint(new Uri("queue:users-queue"));
            await endpoint.Send(new UserAddedEvent() 
            {
               Name = request.Name,
               Surname = request.Surname,
               Patronymic = request.Patronymic,
               Phone = request.Phone,
               Email = request.Email
            });

            logger.LogInformation("Сообщение о создании нового пользователя было успешно отправлено.");

            return await Unit.Task;
        }
    }
}
