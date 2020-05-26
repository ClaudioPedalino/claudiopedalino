using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TotvsChallenge.Data.Contracts;

namespace TotvsChallenge.DataAccess.Repositories
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
