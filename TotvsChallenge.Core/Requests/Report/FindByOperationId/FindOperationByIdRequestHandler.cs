using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TotvsChallenge.Data.Contracts;
using TotvsChallenge.Data.Models;

namespace TotvsChallenge.Core.Requests.Report.FindByOperationId
{
    public class FindOperationByIdRequestHandler : IRequestHandler<FindOperationByIdRequest, OperationInfoModelResponse>
    {
        private readonly IReportRepository reportRepository;

        public FindOperationByIdRequestHandler(IReportRepository reportRepository)
        {
            this.reportRepository = reportRepository ?? throw new System.ArgumentNullException(nameof(reportRepository));
        }

        public async Task<OperationInfoModelResponse> Handle(FindOperationByIdRequest request, CancellationToken cancellationToken)
            => await reportRepository.FindOperationInfoById(request.OperationId);

    }
}
