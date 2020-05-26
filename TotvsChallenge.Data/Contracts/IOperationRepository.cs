using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TotvsChallenge.Entities;

namespace TotvsChallenge.Data.Contracts
{
    public interface IOperationRepository
    {
        //Task<Operation> FindByIdAsync(Guid id);
        Task Add(Operation Operation);
    }
}
