using System;
using TotvsChallengePoC.Data.Models;
using TotvsChallengePoC.Entities;

namespace TotvsChallengePoC.Tests.Stubs
{
    public static class OperationStub
    {
        private const string clientId = "3FA85F64-5717-4562-B3FC-2C963F66AFA1";
        private const string changeId = "44DF17DC-15EC-4CB8-F5EE-08D800333D9F";

        public static OperationInfoModelResponse operation_01 = new OperationInfoModelResponse()
        {
            DateCreated = "25/02/2020",
            ClientFullName = "Stub Man",
            PaymentType = "Cash",
            TotalAmount = 80,
            ChangeReturned = 20
        };

        public static Operation operation_02 = new Operation()
        {
            Id = new Guid(),
            DateCreated = System.DateTimeOffset.Now,
            TotalAmount = 33.09M,
            ClientPaymentAmount = 763.24M,
            PaymentTypeId = 1,
            ClientId = new Guid(clientId),
            ChangeId = new Guid(changeId),
            Change = new Change(Guid.Parse(changeId), 730.15M)
        };
    }
}
