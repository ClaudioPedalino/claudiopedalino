using System.Threading.Tasks;
using TotvsChallengePoC.Data.Models;

namespace TotvsChallengePoC.Data.Contracts
{
    public interface IReportRepository
    {
        //PoC
        //int DummyQuery(string id);

        Task<OperationInfoModelResponse> FindOperationInfoById(string id);
        Task<ClientInfoModelResponse> FindClientInfoById(string id);
    }

}
