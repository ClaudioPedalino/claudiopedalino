using EasyCaching.Core;
using MediatR;
using Newtonsoft.Json;
using Serilog;
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
        private readonly IEasyCachingProvider cachingProvider;
        private readonly IEasyCachingProviderFactory cachingProviderFactory;
        private readonly ILogger logService;

        public FindOperationByIdRequestHandler(IReportRepository reportRepository, IEasyCachingProviderFactory cachingProviderFactory, ILogger logService)
        {
            this.reportRepository = reportRepository ?? throw new System.ArgumentNullException(nameof(reportRepository));
            this.cachingProviderFactory = cachingProviderFactory ?? throw new ArgumentNullException(nameof(cachingProviderFactory));
            this.logService = logService ?? throw new ArgumentNullException(nameof(logService));
            this.cachingProvider = this.cachingProviderFactory.GetCachingProvider("localRedis");
        }

        public async Task<OperationInfoModelResponse> Handle(FindOperationByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await reportRepository.FindOperationInfoById(request.OperationId);
            await SetIntoRedis(request, result);
            return result;
        }

        private async Task SetIntoRedis(FindOperationByIdRequest request, OperationInfoModelResponse result)
        {
            try
            {
                await RegisterCache(request, result);
            }
            catch (Exception ex)
            {
                await Task.Run(() => logService.Error(ex, "Redis isnt avaiable"));
            }
        }

        private Task RegisterCache(FindOperationByIdRequest request, OperationInfoModelResponse result)
        {
            return cachingProvider.SetAsync
                            ("ClientInfoById :" + request.OperationId,
                            JsonConvert.SerializeObject(result),
                            TimeSpan.FromDays(7));
        }

    }
}
