using System.Threading.Tasks;
using TotvsChallengePoC.Entities;

namespace TotvsChallengePoC.Data.Contracts
{
    public interface IClientRepository
    {
        /// <summary>
        /// [EFCore] Find a existing client in database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Client> FindClientByIdAsync(string id);


        /// <summary>
        /// [Dapper] Find a existing client in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Task<string> FindByIdAsync(string id);
    }
}
