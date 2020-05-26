using System;
using System.Data;
using System.Threading.Tasks;

namespace TotvsChallengePoC.Data.Contracts
{
    public interface IBaseRepository
    {
        /// <summary>
        /// Manage the main connection to DB for repositories
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="getData"></param>
        /// <returns></returns>
        Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData);
    }
}
