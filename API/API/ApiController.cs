using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace API
{
    public class ApiController : ControllerBase
    {
        protected readonly IMediator mediator;

        protected ApiController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected async Task<IActionResult> Send(IRequest request, CancellationToken ct = default)
        {
            try
            {
                return new OkObjectResult(await mediator.Send(request, ct));
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }

        protected async Task<IActionResult> Send<TResult>(IRequest<TResult> request, CancellationToken ct = default)
        {
            try
            {
                return new OkObjectResult(await mediator.Send(request, ct));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.ToString());
            }
        }
    }
}
