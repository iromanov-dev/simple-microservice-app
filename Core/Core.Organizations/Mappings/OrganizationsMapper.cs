using AutoMapper;
using Core.Organizations.DTO;
using Data.Models;

namespace Core.Organizations.Mappings
{
    public class OrganizationsMapper : Profile
    {
        public OrganizationsMapper()
        {
            CreateMap<User, UserDto>()
                .ForMember(x => x.OrganizationId, x => x.MapFrom(p => p.Organization.Id))
                .ForMember(x => x.OrganizationName, x => x.MapFrom(p => p.Organization.Name));

            CreateMap<UserDto, User>();
        }
    }
}
