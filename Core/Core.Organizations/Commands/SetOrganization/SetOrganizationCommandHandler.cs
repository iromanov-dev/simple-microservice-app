using Data.Abstractions;
using Data.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Organizations.Commands.SetOrganization
{
    public class SetOrganizationCommandHandler : IRequestHandler<SetOrganizationCommand, Unit>
    {
        private readonly ILogger logger;
        private readonly IGenericRepository<User> userRepository;

        public SetOrganizationCommandHandler(ILogger<SetOrganizationCommandHandler> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            userRepository = unitOfWork.Repository<User>();
        }

        public async Task<Unit> Handle(SetOrganizationCommand request, CancellationToken ct = default)
        {
            logger.LogInformation($"Получен запрос на добавление организации ({request.OrganizationId}) для пользователя: {request.UserId}.");

            var entity = await userRepository.GetAsync(request.UserId);

            entity.OrganizationId = request.OrganizationId;

            userRepository.Update(entity);

            userRepository.SaveChanges();

            logger.LogInformation($"Организация ({request.OrganizationId}) для пользователя {request.UserId} успешно установлена .");

            return await Unit.Task;
        }
    }
}
