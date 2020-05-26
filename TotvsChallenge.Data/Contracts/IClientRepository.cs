using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TotvsChallenge.Data.Contracts
{
    public interface IClientRepository
    {
        //string FindByIdAsync(string id);
        Task<string> FindByIdAsync(string id);
    }
}
