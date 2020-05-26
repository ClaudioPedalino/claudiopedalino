using EasyCaching.Core;
using Moq;
using NUnit.Framework;
using System.Threading;
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

        [SetUp]
        public void Setup()
        {
            clientId = "3FA85F64-5717-4562-B3FC-2C963F66AFA1";
            cancellation = new CancellationToken();
            request = new FindClientInfoByIdRequest(clientId);
            this.reportRepository = new ReportRepositoryMock().reportRepository;
            this.easyCachingProviderFactory = new Mock<IEasyCachingProviderFactory>();
        }

        //[Test]
        //public async Task Test_something()
        //{
        //    //FindClientInfoByIdRequestHandler service = new FindClientInfoByIdRequestHandler(dapperRepository.Object, easyCachingProviderFactory.Object);
        //    //var response = await service.Handle(request, cancellation);

        //    //var result = await this.dapperRepository.Object.FindClientInfoById(clientId);

        //    //Assert.IsNotNull(result);
        //}
    }
}
