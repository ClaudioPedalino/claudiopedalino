using Moq;
using NUnit.Framework;
using TotvsChallengePoC.Data.Contracts;
using TotvsChallengePoC.Tests.Stubs;

namespace TotvsChallengePoC.Tests.MockRepository
{
    public class ReportRepositoryMock
    {
        public Mock<IReportRepository> reportRepository { get; set; }

        public ReportRepositoryMock()
        {
            reportRepository = new Mock<IReportRepository>();
        }

        [SetUp]
        private void Setup()
        {
            reportRepository.Setup(x => x.FindClientInfoById(It.IsAny<string>())).ReturnsAsync(ClientStub.client_01);
            reportRepository.Setup(x => x.FindOperationInfoById(It.IsAny<string>())).ReturnsAsync(OperationStub.operation_01);
        }

        //[Test]
        //public Task Test_something()
        //{
        //    var response = ClientStub.client_01;


        //    Assert.IsNotNull(response);
        //}
    }
}
