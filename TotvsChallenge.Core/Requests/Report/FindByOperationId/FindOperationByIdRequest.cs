using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TotvsChallenge.Data.Models;

namespace TotvsChallenge.Core.Requests.Report.FindByOperationId
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