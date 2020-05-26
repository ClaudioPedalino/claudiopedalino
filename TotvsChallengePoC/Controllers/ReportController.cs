using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TotvsChallengePoC.Core.Request.Reports.FindByOperationId;
using TotvsChallengePoC.Core.Request.Reports.FindClientInfoById;
using TotvsChallengePoC.Data.Models;

namespace TotvsChallengePoC.Api.Controllers
{
    [Route("v1/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator mediator;

        public ReportController(IMediator mediator/*, ILogger<ReportController> logger*/)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("operation/{id}")]
        public async Task<ActionResult<OperationInfoModelResponse>> GetByOperationId(string id)
        {
            var request = new FindOperationByIdRequest(id);
            return Ok(await mediator.Send(request));
        }

        [HttpGet("client/{id}")]
        public async Task<ActionResult<ClientInfoModelResponse>> GetInfoByClientId(string id)
        {
            var request = new FindClientInfoByIdRequest(id);
            return Ok(await mediator.Send(request));
        }

    }
}
