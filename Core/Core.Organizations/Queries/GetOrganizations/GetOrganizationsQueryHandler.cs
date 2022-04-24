using AutoMapper;
using Core.Organizations.DTO;
using Data.Abstractions;
using Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Organizations.Queries.GetOrganizations
{
    public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, OrganizationListDto>
    {
        private readonly IGenericRepository<Organization> repository;
        private readonly IMapper mapper;

        public GetOrganizationsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.repository = unitOfWork.Repository<Organization>();
            this.mapper = mapper;
        }

        public async Task<OrganizationListDto> Handle(GetOrganizationsQuery request, CancellationToken ct = default)
        {
            var organizations = await repository.GetAll().ToListAsync();

            return new OrganizationListDto()
            {
                Organizations = mapper.Map<List<OrganizationDto>>(organizations)
            };
        }
    }
}
