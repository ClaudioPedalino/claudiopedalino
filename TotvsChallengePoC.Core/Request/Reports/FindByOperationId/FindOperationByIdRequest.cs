using MediatR;
using TotvsChallengePoC.Data.Models;

namespace TotvsChallengePoC.Core.Request.Reports.FindByOperationId
{
    public class FindOperationByIdRequest : IRequest<OperationInfoModelResponse>
    {
        public FindOperationByIdRequest(string operationId)
        {
            OperationId = operationId;
        }

        public string OperationId { get; set; }
    }
}
