using Data.Abstractions;
using Data.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Organizations.Commands.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Unit>
    {
        private readonly ILogger logger;
        private readonly IGenericRepository<User> userRepository;
        public AddUserCommandHandler(ILogger<AddUserCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            userRepository = unitOfWork.Repository<User>();
        }

        public async Task<Unit> Handle(AddUserCommand request, CancellationToken ct = default)
        {
            logger.LogInformation($"Получен запрос на добавление пользователя.");

            var entity = new User
            {
                Name = request.Name,
                Surname = request.Surname,
                Patronymic = request.Patronymic,
                Phone = request.Phone,
                Email = request.Email
            };

            userRepository.Add(entity);

            userRepository.SaveChanges();

            logger.LogInformation($"Пользователь ({entity.Id}) успешно сохранен.");

            return await Unit.Task;
        }
    }
}
