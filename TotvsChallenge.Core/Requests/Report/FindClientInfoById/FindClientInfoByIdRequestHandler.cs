using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TotvsChallenge.Data.Contracts;
using TotvsChallenge.Data.Models;

namespace TotvsChallenge.Core.Requests.Report.FindClientInfoById
{
    public class FindClientInfoByIdRequestHandler : IRequestHandler<FindClientInfoByIdRequest, ClientInfoModelResponse>
    {
        private readonly IReportRepository reportRepository;

        public FindClientInfoByIdRequestHandler(IReportRepository reportRepository)
        {
            this.reportRepository = reportRepository ?? throw new System.ArgumentNullException(nameof(reportRepository));
        }

        public async Task<ClientInfoModelResponse> Handle(FindClientInfoByIdRequest request, CancellationToken cancellationToken)
        {
            return await reportRepository.FindClientInfoById(request.ClientId);
        }

    }
}