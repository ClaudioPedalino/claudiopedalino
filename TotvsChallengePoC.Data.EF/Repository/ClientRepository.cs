using Dapper;
using System.Threading.Tasks;
using TotvsChallengePoC.Data.Contracts;

namespace TotvsChallengePoC.Data.EF.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IBaseRepository baseRepository;
        public ClientRepository(IBaseRepository baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public async Task<string> FindByIdAsync(string id)
        {
            return await baseRepository.WithConnection(async q =>
            {
                string query = @"SELECT top 1 id FROM Clients WHERE id = @id ";

                var param = new { id };
                var result = await q.QuerySingleAsync<string>(query, param).ConfigureAwait(false);
                return result;
            });
        }
    }
}
