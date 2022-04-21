using Core.Organizations.DTO;
using MediatR;

namespace Core.Organizations.Commands.SetOrganization
{
    public class SetOrganizationCommand : IRequest<Unit>
    {
        public long UserId { get; set; }
        public long OrganizationId { get; set; }
    }
}
