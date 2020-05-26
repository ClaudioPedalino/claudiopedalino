using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
using TotvsChallengePoC.Data.Contracts;

namespace TotvsChallengePoC.Data.EF
{
    public class BaseRepository : IBaseRepository
    {
        private readonly IConnectionFactory connection;
        public BaseRepository(IConnectionFactory connectionFactory)
        {
            connection = connectionFactory;
        }

        async Task<T> IBaseRepository.WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                using (var cn = connection.CreateConnection)
                {
                    await cn.OpenAsync().ConfigureAwait(false);
                    return await getData(cn).ConfigureAwait(false);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL timeout", ex);
            }
            catch (SqlException ex)
            {
                throw new Exception($"{GetType().FullName}.WithConnection() experienced a SQL exception (not a timeout)", ex);
            }
        }

    }
}
