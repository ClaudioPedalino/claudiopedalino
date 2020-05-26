using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TotvsChallenge.Data.Models;

namespace TotvsChallenge.Data.Contracts
{
    public interface IReportRepository
    {
        //PoC
        //int DummyQuery(string id);

        Task<OperationInfoModelResponse> FindOperationInfoById(string id);
        Task<ClientInfoModelResponse> FindClientInfoById(string id);
    }

}