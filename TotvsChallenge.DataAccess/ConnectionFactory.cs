using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using TotvsChallenge.Data.Contracts;

namespace TotvsChallenge.DataAccess
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
