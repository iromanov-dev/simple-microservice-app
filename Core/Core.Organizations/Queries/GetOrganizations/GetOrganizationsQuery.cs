using Core.Organizations.DTO;
using MediatR;

namespace Core.Organizations.Queries.GetOrganizations
{
    public class GetOrganizationsQuery : IRequest<OrganizationListDto>
    {
    }
}
