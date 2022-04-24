using AutoMapper;
using Core.Organizations.DTO;
using Data.Abstractions;
using Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Organizations.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, UsersListDto>
    {
        private readonly IGenericRepository<User> repository;
        private readonly IMapper mapper;

        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repository = unitOfWork.Repository<User>();
            this.mapper = mapper;
        }

        public async Task<UsersListDto> Handle(GetAllUsersQuery request, CancellationToken ct = default)
        {
            var users = await repository.GetAll().ToListAsync();

            return new UsersListDto()
            {
                Users = mapper.Map<List<UserDto>>(users)
            };
        }
    }
}
