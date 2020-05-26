using TotvsChallengePoC.Data.Models;

namespace TotvsChallengePoC.Tests.Stubs
{
    public static class ClientStub
    {
        public static ClientInfoModelResponse client_01 = new ClientInfoModelResponse()
        {
            ClientFullName = "Stub Man",
            TotalSpend = 120.30M,
            CashTimes = 3,
            CreditTimes = 2,
            DebitTimes = 1
        };
    }
}
