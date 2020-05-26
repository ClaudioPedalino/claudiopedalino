using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using TotvsChallengePoC.Data.Contracts;

namespace TotvsChallengePoC.Data.EF
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _config;

        public ConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection CreateConnection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("Sql"));
            }
        }


    }
}
