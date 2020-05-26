using System.Threading.Tasks;

namespace TotvsChallengePoC.Data.Contracts
{
    public interface IClientRepository
    {
        //string FindByIdAsync(string id);
        Task<string> FindByIdAsync(string id);
    }
}
