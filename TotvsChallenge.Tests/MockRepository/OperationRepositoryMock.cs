using Moq;
using TotvsChallengePoC.Data.Repositories;
using TotvsChallengePoC.Entities;

namespace TotvsChallengePoC.Tests.MockRepository
{
    public class OperationRepositoryMock
    {
        public Mock<IOperationRepository> operationRepository { get; set; }

        public OperationRepositoryMock()
        {
            operationRepository = new Mock<IOperationRepository>();
        }

        private void Setup()
        {
            operationRepository.Setup(x => x.Add(It.IsAny<Operation>()));
        }
    }
}
