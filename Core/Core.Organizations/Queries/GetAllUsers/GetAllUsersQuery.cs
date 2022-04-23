using Core.Organizations.DTO;
using MediatR;

namespace Core.Organizations.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<UsersListDto>
    {
    }
}
