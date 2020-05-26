using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace TotvsChallenge.Data.Contracts
{
    public interface IBaseRepository
    {
        Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData);
    }
}
