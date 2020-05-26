using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotvsChallenge.Core.Request.Operations;

namespace TovusChallenge.Api.Controllers
{
    [Route("v1/operation")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        private readonly IMediator mediator;

        public OperationController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("buy")]
        public async Task<ActionResult> Post([FromBody]BuyRequest request)
        {
            return Ok(await mediator.Send(request));
        }

    }
}