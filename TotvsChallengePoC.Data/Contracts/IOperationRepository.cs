using System.Threading.Tasks;
using TotvsChallengePoC.Entities;

namespace TotvsChallengePoC.Data.Repositories
{
    public interface IOperationRepository
    {
        //Task<Operation> FindByIdAsync(Guid id);
        Task Add(Operation Operation);
    }
}
