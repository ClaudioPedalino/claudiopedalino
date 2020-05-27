using EasyCaching.Core;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Serilog;
using System.Threading;
using System.Threading.Tasks;
using TotvsChallengePoC.Core.Request.Reports.FindClientInfoById;
using TotvsChallengePoC.Data.Contracts;
using TotvsChallengePoC.Tests.MockRepository;

namespace TotvsChallengePoC.Test
{
    public class MethodTest
    {
        string clientId;
        CancellationToken cancellation;
        FindClientInfoByIdRequest request;
        Mock<IReportRepository> reportRepository;
        Mock<IEasyCachingProviderFactory> easyCachingProviderFactory;
        Mock<ILogger> logService;
        Mock<IConfiguration> _config;


        [SetUp]
        public void Setup()
        {
            clientId = "3FA85F64-5717-4562-B3FC-2C963F66AFA1";
            cancellation = new CancellationToken();
            request = new FindClientInfoByIdRequest(clientId);
            reportRepository = new ReportRepositoryMock().reportRepository;
            easyCachingProviderFactory = new Mock<IEasyCachingProviderFactory>();
            logService = new Mock<ILogger>();
            _config = new Mock<IConfiguration>();
        }

        [Test]
        public async Task Test_something()
        {
            FindClientInfoByIdRequestHandler service = new FindClientInfoByIdRequestHandler(reportRepository.Object, easyCachingProviderFactory.Object, logService.Object, _config.Object);
            var response = await service.Handle(request, cancellation);

            var result = await reportRepository.Object.FindClientInfoById(clientId);

            Assert.IsNotNull(result);
        }
    }
}
