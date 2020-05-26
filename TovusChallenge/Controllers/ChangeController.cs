using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotvsChallenge.Core.Requests.CalculateChange;

namespace TovusChallenge.Api.Controllers
{
    [Route("v1/transaction")]
    [ApiController]
    public class ChangeController : ControllerBase
    {
        private readonly IMediator mediator;

        public ChangeController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // [Authorize(nameof(CalculateChangeRequest))]
        [HttpPost("buy")]
        public async Task<ActionResult> Post([FromBody]CalculateChangeRequest request)
        {
            return Ok(await mediator.Send(request));
        }

    }
}