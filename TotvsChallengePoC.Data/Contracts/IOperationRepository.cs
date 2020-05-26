using System.Threading.Tasks;
using TotvsChallengePoC.Entities;

namespace TotvsChallengePoC.Data.Repositories
{
    public interface IOperationRepository
    {
        /// <summary>
        /// Create a new Operation entry in database and the data associate
        /// </summary>
        /// <param name="Operation"></param>
        /// <returns></returns>
        Task Add(Operation Operation);
    }
}
