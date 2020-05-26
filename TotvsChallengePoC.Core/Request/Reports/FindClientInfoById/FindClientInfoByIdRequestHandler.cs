﻿using EasyCaching.Core;
using MediatR;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using TotvsChallengePoC.Data.Contracts;
using TotvsChallengePoC.Data.Models;

namespace TotvsChallengePoC.Core.Request.Reports.FindClientInfoById
{
    public class FindClientInfoByIdRequestHandler : IRequestHandler<FindClientInfoByIdRequest, ClientInfoModelResponse>
    {
        private readonly IReportRepository reportRepository;
        private readonly IEasyCachingProvider cachingProvider;
        private readonly IEasyCachingProviderFactory cachingProviderFactory;
        private readonly ILogger logService;

        public FindClientInfoByIdRequestHandler(IReportRepository reportRepository, IEasyCachingProviderFactory cachingProviderFactory, ILogger logService)
        {
            this.reportRepository = reportRepository ?? throw new System.ArgumentNullException(nameof(reportRepository));
            this.cachingProviderFactory = cachingProviderFactory ?? throw new ArgumentNullException(nameof(cachingProviderFactory));
            this.logService = logService ?? throw new ArgumentNullException(nameof(logService));
            this.cachingProvider = this.cachingProviderFactory.GetCachingProvider("localRedis"); //TODO:Move to config
        }


        public async Task<ClientInfoModelResponse> Handle(FindClientInfoByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await reportRepository.FindClientInfoById(request.ClientId);
            await SetIntoRedis(request, result);
            return result;
        }

        #region Redis

        private async Task SetIntoRedis(FindClientInfoByIdRequest request, ClientInfoModelResponse result)
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

        //private Task<T> RegisterCache<T, S>(T request, S result) where S : IRequest<T>
        private Task RegisterCache(FindClientInfoByIdRequest request, ClientInfoModelResponse result)
        {
            return cachingProvider.SetAsync
                            ("ClientInfoById :" + request.ClientId,
                            JsonConvert.SerializeObject(result),
                            TimeSpan.FromDays(7));
        }
        #endregion
    }


}
