using Core.Organizations.DTO;
using MediatR;

namespace Core.Organizations.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<UsersListDto>
    {
        public long OrganizationId { get; set; }
        public int Page { get; set; }
        public int RowsPerPage { get; set; }
    }
}
