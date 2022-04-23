using System.Collections.Generic;

namespace Core.Organizations.DTO
{
    public class OrganizationListDto
    {
        public IEnumerable<OrganizationDto> Organizations { get; set; }
    }
}
