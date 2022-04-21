using Core.Users.Commands.AddUser;
using API;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ApiController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {}

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserCommand request, CancellationToken ct = default)
        {
            return await Send(request, ct);
        }
    }
}
