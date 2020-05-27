using EasyCaching.Core;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Serilog;
using System.Threading;
using System.Threading.Tasks;
using TotvsChallengePoC.Core.Request.Reports.FindByOperationId;
using TotvsChallengePoC.Core.Request.Reports.FindClientInfoById;
using TotvsChallengePoC.Data.Contracts;
using TotvsChallengePoC.Entities;
using TotvsChallengePoC.Tests.MockRepository;

namespace TotvsChallengePoC.Test
{
    public class ReportsTest
    {
        string clientId;
        string operationId;
        CancellationToken cancellation;
        FindClientInfoByIdRequest requestClientId;
        FindOperationByIdRequest requestOperationId;
        Mock<IReportRepository> reportRepository;
        Mock<IEasyCachingProviderFactory> easyCachingProviderFactory;
        Mock<ILogger> logService;
        Mock<IConfiguration> _config;


        [SetUp]
        public void Setup()
        {
            clientId = "3FA85F64-5717-4562-B3FC-2C963F66AFA1";
            cancellation = new CancellationToken();
            requestClientId = new FindClientInfoByIdRequest(clientId);
            requestOperationId = new FindOperationByIdRequest(operationId);
            reportRepository = new ReportRepositoryMock().reportRepository;
            easyCachingProviderFactory = new Mock<IEasyCachingProviderFactory>();
            logService = new Mock<ILogger>();
            _config = new Mock<IConfiguration>();
        }

        [Test]
        //[TestCase("expected", "input")]
        //public void Valid_ClientID_Request(string expected, string input)
        public async Task Valid_ClientId_Request()
        {
            FindClientInfoByIdRequestHandler service = new FindClientInfoByIdRequestHandler(reportRepository.Object, easyCachingProviderFactory.Object, logService.Object, _config.Object);
            var response = await service.Handle(requestClientId, cancellation);

            var result = await reportRepository.Object.FindClientInfoById(clientId);

            Assert.AreEqual(result, response);
        }


        [Test]
        public async Task Valid_OperationId_Request()
        {
            FindOperationByIdRequestHandler service = new FindOperationByIdRequestHandler(reportRepository.Object, easyCachingProviderFactory.Object, logService.Object, _config.Object);
            var response = await service.Handle(requestOperationId, cancellation);

            var result = await reportRepository.Object.FindOperationInfoById(operationId);

            Assert.AreEqual(result, response);
        }


    }
}
