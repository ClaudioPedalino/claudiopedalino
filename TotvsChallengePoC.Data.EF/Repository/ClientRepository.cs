using System;
using System.Threading.Tasks;
using TotvsChallengePoC.Data.Contracts;
using TotvsChallengePoC.Entities;

namespace TotvsChallengePoC.Data.EF.Repository
{
    public class ClientRepository : IClientRepository
    {
        //private readonly IBaseRepository baseRepository;
        private readonly DataContext dataContext;

        public ClientRepository(/*IBaseRepository baseRepository,*/ DataContext dataContext)
        {
            //this.baseRepository = baseRepository ?? throw new System.ArgumentNullException(nameof(baseRepository));
            this.dataContext = dataContext ?? throw new System.ArgumentNullException(nameof(dataContext));
        }

        //EFCore way
        public async Task<Client> FindClientByIdAsync(string id)
            => await dataContext.Clients.FindAsync(new Guid(id));


        //DapperWay
        //public async Task<string> FindByIdAsync(string id)
        //{
        //    return await baseRepository.WithConnection(async q =>
        //    {
        //        string query = @"SELECT top 1 id FROM Clients WHERE id = @id ";

        //        var param = new { id };
        //        var result = await q.QuerySingleAsync<string>(query, param).ConfigureAwait(false);
        //        return result;
        //    });
        //}
    }
}
