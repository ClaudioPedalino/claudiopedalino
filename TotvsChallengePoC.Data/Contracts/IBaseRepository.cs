using System;
using System.Data;
using System.Threading.Tasks;

namespace TotvsChallengePoC.Data.Contracts
{
    public interface IBaseRepository
    {
        Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData);
    }
}
