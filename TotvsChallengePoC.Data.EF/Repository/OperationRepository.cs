using System;
using System.Threading.Tasks;
using TotvsChallengePoC.Data.Repositories;
using TotvsChallengePoC.Entities;

namespace TotvsChallengePoC.Data.EF.Repository
{
    public class OperationRepository : IOperationRepository
    {
        private readonly DataContext dataContext;

        public OperationRepository(DataContext dataContext)
            => this.dataContext = dataContext
                ?? throw new ArgumentNullException(nameof(dataContext));

        public async Task Add(Operation operation)
        {
            await dataContext.AddAsync(operation);
            await dataContext.SaveChangesAsync();
        }

    }
}
