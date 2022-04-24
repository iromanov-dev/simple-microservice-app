using AutoMapper;
using Core.Organizations.DTO;
using Data.Models;
using Data.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Organizations.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, UsersListDto>
    {
        private readonly IGenericRepository<User> repository;
        private readonly IMapper mapper;

        public GetUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repository = unitOfWork.Repository<User>();
            this.mapper = mapper;
        }

        public async Task<UsersListDto> Handle(GetUsersQuery request, CancellationToken ct = default)
        {
            var users = await repository.GetAll().Where(x => x.OrganizationId == request.OrganizationId).Include(x => x.Organization).Skip((request.Page - 1) * request.RowsPerPage).Take(request.RowsPerPage).ToListAsync();

            return new UsersListDto()
            {
                Users = mapper.Map<List<UserDto>>(users)            
            };
        }
    }
}
