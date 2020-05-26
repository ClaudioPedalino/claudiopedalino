using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TotvsChallenge.Data.Contracts;
using TotvsChallenge.Entities;

namespace TotvsChallenge.DataAccess.EF.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly DataContext dataContext;

        public OperationRepository(DataContext dataContext)
            => this.dataContext = dataContext
                ?? throw new ArgumentNullException(nameof(dataContext));

        //public async Task<Operation> FindByIdAsync(Guid id)
        //    => await dataContext.Operations.FindAsync(id);

        public async Task Add(Operation operation)
        {
            await dataContext.AddAsync(operation);
            await dataContext.SaveChangesAsync();
        }

    }
}
