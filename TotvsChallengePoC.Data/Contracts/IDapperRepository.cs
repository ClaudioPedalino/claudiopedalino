using System.Threading.Tasks;
using TotvsChallengePoC.Data.Models;

namespace TotvsChallengePoC.Data.Contracts
{
    public interface IReportRepository
    {
        /// <summary>
        /// Find a Operation by Id in dbo.Operations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OperationInfoModelResponse> FindOperationInfoById(string id);

        /// <summary>
        /// Find a Client by Id in dbo.Clients
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ClientInfoModelResponse> FindClientInfoById(string id);
    }

}
