using API;
using Core.Organizations.Commands.SetOrganization;
using Core.Organizations.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ApiController
    {
        public OrganizationsController(IMediator mediator) : base(mediator)
        {}

        [HttpPost]
        public async Task<IActionResult> SetOrganization([FromBody] SetOrganizationCommand request, CancellationToken ct = default)
        {
            return await Send(request, ct);
        }

        [HttpPost]
        [Route("users")]
        public async Task<IActionResult> GetUsers([FromBody] GetUsersQuery request, CancellationToken ct = default)
        {
            return await Send(request, ct);
        }
    }
}
